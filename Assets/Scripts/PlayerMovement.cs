using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Input _input;
    private Vector2 _moveInput;
    private Rigidbody _rb;
    [SerializeField] private float speed = 10f; 
    private void Awake()
    {
        _input = new Input();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
        _input.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y) * Time.deltaTime;
        _rb.AddForce(move * speed, ForceMode.Force);
    }
}