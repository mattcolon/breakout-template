using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Attach to the Paddle GameObject. Handles left/right movement (A/D or left/right arrow)
/// at a configurable speed, and ball launch via the Input System (Player/Jump, bound to Space):
/// fires the ball up at 45° randomly left or right.
/// </summary>
public class PaddleController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField]
    GameObject ball;

    [SerializeField]
    float launchSpeed = 5f;

    [SerializeField]
    float moveSpeed = 12f;

    InputSystem_Actions _actions;
    InputSystem_Actions.PlayerActions _playerActions;
    Vector3 _ballStartPosition;
    Vector2 _moveInput;
    Rigidbody2D _rb;

    void Awake()
    {
        _actions = new InputSystem_Actions();
        _playerActions = _actions.Player;
        _playerActions.AddCallbacks(this);
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (ball != null)
        {
            _ballStartPosition = ball.transform.position;
            ball.SetActive(false);
        }
    }

    void OnEnable()
    {
        _playerActions.Enable();
    }

    void OnDisable()
    {
        _playerActions.Disable();
    }

    void OnDestroy()
    {
        _actions?.Dispose();
    }

    void FixedUpdate()
    {
        float dx = _moveInput.x * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + Vector2.right * dx);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || ball == null || ball.activeSelf)
            return;

        ball.SetActive(true);
        ball.transform.position = new Vector3(
            transform.position.x,
            _ballStartPosition.y,
            _ballStartPosition.z
        );

        Vector2 direction;
        direction.x = Random.value < 0.5f ? -1f : 1f;
        direction.y = 1f;
        direction.Normalize();

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * launchSpeed;
    }

    public void OnLook(InputAction.CallbackContext context) { }

    public void OnAttack(InputAction.CallbackContext context) { }

    public void OnInteract(InputAction.CallbackContext context) { }

    public void OnCrouch(InputAction.CallbackContext context) { }

    public void OnPrevious(InputAction.CallbackContext context) { }

    public void OnNext(InputAction.CallbackContext context) { }

    public void OnSprint(InputAction.CallbackContext context) { }
}
