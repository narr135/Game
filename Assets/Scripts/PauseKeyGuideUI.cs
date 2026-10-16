using System.Collections.Generic;
using UnityEngine;

public class PauseKeyGuideUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private KeyGuideRowUI rowPrefab;

    [Header("Containers")]
    [SerializeField] private Transform player1Container;
    [SerializeField] private Transform player2Container;

    private static readonly (string label, string map, string action, int idx)[] Player1Rows =
    {
        ("Run Left",  "Player1", "Move",   1),
        ("Run Right", "Player1", "Move",   2),
        ("Jump",      "Player1", "Jump",   0),
        ("Crouch",    "Player1", "Crouch", 0),
        ("Fire",      "Player1", "Fire",   0),
    };

    private static readonly (string label, string map, string action, int idx)[] Player2Rows =
    {
        ("Run Left",  "Player2", "Move",   1),
        ("Run Right", "Player2", "Move",   2),
        ("Jump",      "Player2", "Jump",   0),
        ("Crouch",    "Player2", "Crouch", 0),
        ("Fire",      "Player2", "Fire",   0),
    };

    private readonly List<KeyGuideRowUI> allRows = new List<KeyGuideRowUI>();
    private bool built;

    private void OnEnable()
    {
        if (InputRebinder.Instance != null)
            InputRebinder.Instance.OnRebindsChanged += RefreshAll;

        BuildIfNeeded();
        RefreshAll();
    }

    private void OnDisable()
    {
        if (InputRebinder.Instance != null)
            InputRebinder.Instance.OnRebindsChanged -= RefreshAll;
    }

    private void BuildIfNeeded()
    {
        if (built) return;

        SpawnRows(Player1Rows, player1Container);
        SpawnRows(Player2Rows, player2Container);
        built = true;
    }

    private void SpawnRows((string label, string map, string action, int idx)[] defs, Transform parent)
    {
        foreach (var def in defs)
        {
            var row = Instantiate(rowPrefab, parent);
            row.Setup(def.map, def.action, def.idx, def.label);
            allRows.Add(row);
        }
    }

    public void RefreshAll()
    {
        foreach (var r in allRows)
            if (r != null) r.RefreshLabel();
    }
}