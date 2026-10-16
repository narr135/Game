using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebindRowUI : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private TextMeshProUGUI actionLabel;
    [SerializeField] private Button rebindButton;
    [SerializeField] private TextMeshProUGUI bindingLabel;
    [SerializeField] private Image buttonBackground;

    private string mapName;
    private string actionName;
    private int bindingIndex;

    public void Setup(string mapName, string actionName, int bindingIndex, string displayName)
    {
        this.mapName = mapName;
        this.actionName = actionName;
        this.bindingIndex = bindingIndex;

        if (actionLabel != null)
            actionLabel.text = displayName;

        if (rebindButton != null)
        {
            rebindButton.onClick.RemoveAllListeners();
            rebindButton.onClick.AddListener(OnRebindClicked);
        }

        RefreshLabel();
    }

    public void RefreshLabel()
    {
        if (bindingLabel == null) return;

        var rebinder = InputRebinder.Instance;
        if (rebinder == null)
        {
            bindingLabel.text = "?";
            return;
        }

        bindingLabel.text = rebinder.GetBindingDisplay(mapName, actionName, bindingIndex);
    }

    private void OnRebindClicked()
    {
        var rebinder = InputRebinder.Instance;
        rebinder.StartRebind(mapName, actionName, bindingIndex);
    }
}