using TMPro;
using UnityEngine;

public class KeyGuideRowUI : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private TextMeshProUGUI actionLabel;
    [SerializeField] private TextMeshProUGUI keyLabel;

    private string mapName;
    private string actionName;
    private int bindingIndex;

    public void Setup(string mapName, string actionName, int bindingIndex, string displayName)
    {
        this.mapName = mapName;
        this.actionName = actionName;
        this.bindingIndex = bindingIndex;

        if (actionLabel != null) actionLabel.text = displayName;
        RefreshLabel();
    }

    public void RefreshLabel()
    {
        if (keyLabel == null) return;

        var rebinder = InputRebinder.Instance;
        if (rebinder == null || rebinder.Actions == null)
        {
            keyLabel.text = "?";
            return;
        }

        keyLabel.text = rebinder.GetBindingDisplay(mapName, actionName, bindingIndex);
    }
}