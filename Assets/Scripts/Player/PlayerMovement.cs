using System.Collections;
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
    [SerializeField] private float drag;
    private bool _controlsInverted = false; 

    private void Awake()
    {
        _input = new Input();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (!_input.Player.enabled)
        {
            _input.Player.Enable();
        }

        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
        if (_input.Player.enabled)
        {
            _input.Player.Disable();
        }
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
        //CheckPlayerPos();
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 move;
        if (!_controlsInverted)
        {
            move = new Vector3(_moveInput.x, 0, _moveInput.y) * Time.deltaTime;
        }
        else
        {
            move = new Vector3(-_moveInput.x, 0, -_moveInput.y) * Time.deltaTime;
        }
       
        _rb.AddForce(move * speed, ForceMode.Force);
        // Apply drag to reduce the velocity over time
        Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        _rb.AddForce(-horizontalVelocity * drag, ForceMode.Acceleration);
    }

    void Rotate()
    {
        // Calculate current move direction
        Vector3 moveDirection;
        if (!_controlsInverted)
        {
            moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        }
        else
        {
            moveDirection = new Vector3(-_moveInput.x, 0, -_moveInput.y);
        }

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


    private void OnTriggerEnter(Collider other)
    {
        // On trigger with Exit layer mask
        if (other.gameObject.layer == LayerMask.NameToLayer("Exit") && Camera.main.GetComponent<CameraController>().GetAlchemyRoom())
        {
            // Teleport the player to the alchemy room
            Camera.main.GetComponent<CameraController>().SetAlchemyRoom(false);
            StartCoroutine(MovePlayerToCustomers());

        }
        else
        {
            Camera.main.GetComponent<CameraController>().SetAlchemyRoom(true);
            StartCoroutine(MovePlayerToAlchemy());

        }
    }

    private void CheckPlayerPos()
    {
        if (transform.position.x < -16f && Camera.main.GetComponent<CameraController>().GetAlchemyRoom())
        {
            Camera.main.GetComponent<CameraController>().SetAlchemyRoom(false);
            // Start couroutine to move player to the left for a few seconds
        }
        else
        {
            Camera.main.GetComponent<CameraController>().SetAlchemyRoom(true);

        }
    }
    
    public void SetControlsInverted(bool value)
    {
        _controlsInverted = value;
    }
    private IEnumerator MovePlayerToCustomers()
    {
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MovePlayerToAlchemy()
    {

        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            transform.position += new Vector3(10, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }
}