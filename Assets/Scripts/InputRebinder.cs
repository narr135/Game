using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class InputRebinder : MonoBehaviour
{
    public static InputRebinder Instance { get; private set; }

    [SerializeField] private InputActionAsset inputActions;

    [Header("UI Feedback")]
    [SerializeField] private GameObject rebindOverlay;
    [SerializeField] private TextMeshProUGUI rebindText;

    [Header("Settings")]
    [SerializeField] private string rebindsPrefsKey = "inputRebinds";

    public InputActionAsset Actions => inputActions;

    public event Action OnRebindsChanged;

    private InputActionRebindingExtensions.RebindingOperation currentRebind;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (rebindOverlay != null) rebindOverlay.SetActive(false);
        LoadRebinds();
    }

    public void StartRebind(string mapName, string actionName, int bindingIndex)
    {

        var action = inputActions.FindActionMap(mapName)?.FindAction(actionName);
        if (action == null)
        {
            Debug.LogWarning($"Action not found: {mapName}/{actionName}");
            return;
        }

        currentRebind?.Cancel();

        if (rebindOverlay != null) rebindOverlay.SetActive(true);
        if (rebindText != null) rebindText.text = $"Press a key for {actionName}...";

        action.Disable();

        currentRebind = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(op => FinishRebind(action, bindingIndex, op, true))
            .OnCancel(op => FinishRebind(action, bindingIndex, op, false))
            .Start();
    }

    private void FinishRebind(InputAction action, int bindingIndex,
                              InputActionRebindingExtensions.RebindingOperation operation,
                              bool success)
    {
        operation.Dispose();
        currentRebind = null;

        if (rebindOverlay != null) rebindOverlay.SetActive(false);
        action.Enable();

        if (success)
        {
            SaveRebinds();
            OnRebindsChanged?.Invoke();
        }
    }

    public void SaveRebinds()
    {
        if (inputActions == null) return;
        string json = inputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(rebindsPrefsKey, json);
        PlayerPrefs.Save();
        Debug.Log($"Saved overrides:\n{json}");
    }

    public void LoadRebinds()
    {
        if (inputActions == null) return;
        if (!PlayerPrefs.HasKey(rebindsPrefsKey)) return;

        string json = PlayerPrefs.GetString(rebindsPrefsKey);
        if (string.IsNullOrEmpty(json)) return;

        inputActions.LoadBindingOverridesFromJson(json);
        Debug.Log($"Loaded overrides:\n{json}");

        OnRebindsChanged?.Invoke();
    }

    public void ResetAllBindings()
    {
        if (inputActions == null) return;

        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(rebindsPrefsKey);
        PlayerPrefs.Save();
        Debug.Log("All bindings reset to default.");

        OnRebindsChanged?.Invoke();
    }

    private static readonly Dictionary<string, string> ShortNames = new Dictionary<string, string>
    {
        { "Left Ctrl",     "L.Ctrl" },
        { "Right Ctrl",    "R.Ctrl" },
        { "Left Control",  "L.Ctrl" },
        { "Right Control", "R.Ctrl" },
        { "Left Shift",    "L.Shift" },
        { "Right Shift",   "R.Shift" },
        { "Left Alt",      "L.Alt" },
        { "Right Alt",     "R.Alt" },
        { "Left Arrow",    "Left" },
        { "Right Arrow",   "Right" },
        { "Up Arrow",      "Up" },
        { "Down Arrow",    "Down" },
        { "Page Up",       "PgUp" },
        { "Page Down",     "PgDn" },
        { "Backspace",     "Bksp" },
        { "Enter",         "Enter" },
        { "Numpad Enter",  "NumEnter" },
        { "Space",         "Space" }, 
    };

    public string GetBindingDisplay(string mapName, string actionName, int bindingIndex)
    {
        var action = inputActions?.FindActionMap(mapName)?.FindAction(actionName);
        if (action == null) return "?";

        string raw = action.GetBindingDisplayString(bindingIndex);

        int colon = raw.IndexOf(':');
        if (colon >= 0 && colon + 1 < raw.Length)
            raw = raw.Substring(colon + 1).Trim();

        return ShortNames.TryGetValue(raw, out var shortName) ? shortName : raw;
    }
    private void OnDestroy()
    {
        currentRebind?.Dispose();
        if (Instance == this) Instance = null;
    }
}