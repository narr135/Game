using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [Header("Input Actions Asset")]
    [SerializeField] private InputActionAsset inputActions;

    private InputActionMap player1Map;
    private InputActionMap player2Map;
    private InputActionMap uiMap;

    // Player 1
    public float Move1 { get; private set; }
    public bool Jump1Pressed { get; private set; }
    public bool Crouch1Held { get; private set; }
    public bool Fire1Pressed { get; private set; }
    public bool Fire1Held { get; private set; }

    // Player 2
    public float Move2 { get; private set; }
    public bool Jump2Pressed { get; private set; }
    public bool Crouch2Held { get; private set; }
    public bool Fire2Pressed { get; private set; }
    public bool Fire2Held { get; private set; }

    // UI
    public bool CancelPressed { get; private set; }
    public bool BothFireHeld => Fire1Held && Fire2Held;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        player1Map = inputActions.FindActionMap("Player1", throwIfNotFound: true);
        player2Map = inputActions.FindActionMap("Player2", throwIfNotFound: true);
        uiMap      = inputActions.FindActionMap("UI",      throwIfNotFound: true);

        // Player 1
        player1Map.FindAction("Move").performed   += ctx => Move1 = ctx.ReadValue<float>();
        player1Map.FindAction("Move").canceled    += ctx => Move1 = 0f;
        player1Map.FindAction("Jump").performed   += ctx => Jump1Pressed = true;
        player1Map.FindAction("Crouch").performed += ctx => Crouch1Held = true;
        player1Map.FindAction("Crouch").canceled  += ctx => Crouch1Held = false;
        player1Map.FindAction("Fire").performed   += ctx => { Fire1Pressed = true; Fire1Held = true; };
        player1Map.FindAction("Fire").canceled    += ctx => Fire1Held = false;

        // Player 2
        player2Map.FindAction("Move").performed   += ctx => Move2 = ctx.ReadValue<float>();
        player2Map.FindAction("Move").canceled    += ctx => Move2 = 0f;
        player2Map.FindAction("Jump").performed   += ctx => Jump2Pressed = true;
        player2Map.FindAction("Crouch").performed += ctx => Crouch2Held = true;
        player2Map.FindAction("Crouch").canceled  += ctx => Crouch2Held = false;
        player2Map.FindAction("Fire").performed   += ctx => { Fire2Pressed = true; Fire2Held = true; };
        player2Map.FindAction("Fire").canceled    += ctx => Fire2Held = false;

        // UI
        uiMap.FindAction("Cancel").performed += ctx => CancelPressed = true;
    }

    private void OnEnable()
    {
        player1Map?.Enable();
        player2Map?.Enable();
        uiMap?.Enable();
    }

    private void OnDisable()
    {
        player1Map?.Disable();
        player2Map?.Disable();
        uiMap?.Disable();
    }

    private void LateUpdate()
    {
        Jump1Pressed = false;
        Jump2Pressed = false;
        Fire1Pressed = false;
        Fire2Pressed = false;
        CancelPressed = false;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}