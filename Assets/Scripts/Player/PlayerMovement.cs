using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Input _input;
    private Vector2 _moveInput;
    private Rigidbody _rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 15f;
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
        Rotate();
    }

    void Move()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y) * Time.deltaTime;
        _rb.AddForce(move * speed, ForceMode.Force);
    }

    void Rotate()
    {
        // Calculate current move direction
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

        // If move direction is not zero
        if (moveDirection != Vector3.zero)
        {
            // Calculate the target rotation based on move direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // Smoothly rotate the orientation towards the target direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);

    }
}