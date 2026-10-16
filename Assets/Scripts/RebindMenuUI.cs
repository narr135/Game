using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebindMenuUI : MonoBehaviour
{
    [Header("Prefab + Container")]
    [SerializeField] private RebindRowUI rowPrefab;
    [SerializeField] private Transform rowContainer;

    [Header("Buttons")]
    [SerializeField] private Button resetAllButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject pauseMenuPanel;

    private readonly List<RebindRowUI> spawnedRows = new List<RebindRowUI>();

    private static readonly (string map, string action, int idx, string label)[] RowDefinitions =
    {
        // Player 1
        ("Player1", "Move",   1, "P1  Move Left"),
        ("Player1", "Move",   2, "P1  Move Right"),
        ("Player1", "Jump",   0, "P1  Jump"),
        ("Player1", "Crouch", 0, "P1  Crouch"),
        ("Player1", "Fire",   0, "P1  Fire"),

        // Player 2
        ("Player2", "Move",   1, "P2  Move Left"),
        ("Player2", "Move",   2, "P2  Move Right"),
        ("Player2", "Jump",   0, "P2  Jump"),
        ("Player2", "Crouch", 0, "P2  Crouch"),
        ("Player2", "Fire",   0, "P2  Fire"),
    };

    private void Awake()
    {
        if (resetAllButton != null)
            resetAllButton.onClick.AddListener(OnResetAllClicked);

        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnEnable()
    {
        if (InputRebinder.Instance != null)
            InputRebinder.Instance.OnRebindsChanged += RefreshAllRows;

        BuildRowsIfNeeded();
        RefreshAllRows();
    }

    private void OnDisable()
    {
        if (InputRebinder.Instance != null)
            InputRebinder.Instance.OnRebindsChanged -= RefreshAllRows;
    }

    private void BuildRowsIfNeeded()
    {
        if (spawnedRows.Count > 0) return;

        foreach (var def in RowDefinitions)
        {
            var row = Instantiate(rowPrefab, rowContainer);
            row.Setup(def.map, def.action, def.idx, def.label);
            spawnedRows.Add(row);
        }
    }

    public void RefreshAllRows()
    {
        foreach (var row in spawnedRows)
        {
            if (row != null) row.RefreshLabel();
        }
    }

    private void OnResetAllClicked()
    {
        InputRebinder.Instance.ResetAllBindings();
    }

    private void OnBackClicked()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}