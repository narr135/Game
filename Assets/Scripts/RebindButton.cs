using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RebindButton : MonoBehaviour
{
    [Header("Binding Info")]
    [SerializeField] private string actionMapName = "Player1";
    [SerializeField] private string actionName = "Jump";
    [SerializeField] private int bindingIndex = 0;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        InputRebinder.Instance.StartRebind(actionMapName, actionName, bindingIndex);
    }

    private void OnDestroy()
    {
        if (button != null) button.onClick.RemoveListener(OnClick);
    }
}