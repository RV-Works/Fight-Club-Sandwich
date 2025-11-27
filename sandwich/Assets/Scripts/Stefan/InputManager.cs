//using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void buttonDelegate();
    public delegate void vector2Delegate(Vector2 moveInput);

    [SerializeField] private PlayerInput playerInput;
    private InputActionAsset inputAsset;
    private InputActionMap player;

    private Vector2 m_moveInput = new Vector2();
    public Vector2 MoveInput
    {
        get { return m_moveInput; }
    }

    public event buttonDelegate dashEvent;
    public event buttonDelegate useItemEvent;
    public event vector2Delegate moveEvent;

    private void Awake()
    {

        inputAsset = playerInput.actions;

        player = inputAsset.FindActionMap("Player");
    }

    void Start()
    {
        // movement
        player.FindAction("Move").performed += OnMove;
        player.FindAction("Move").canceled += OnMove;

        // buttons
        player.FindAction("Dash").performed += OnDash;
        player.FindAction("Attack").performed += OnAttack;

        player.Enable();
    }

    private void OnDisable()
    {
        // values
        player.FindAction("Move").performed -= OnMove;
        player.FindAction("Move").canceled -= OnMove;

        // buttons
        player.FindAction("Dash").performed -= OnDash;
        player.FindAction("Attack").performed -= OnAttack;

        player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_moveInput = context.ReadValue<Vector2>();
        moveEvent?.Invoke(m_moveInput);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        // if any are connected invoke
        dashEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // if connected invoke
        useItemEvent?.Invoke();
    }
}
