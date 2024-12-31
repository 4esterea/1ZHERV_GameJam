using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // The player's Transform
    [SerializeField] private float cameraDrag = 10f; // The drag/smoothness of the camera movement
    [SerializeField] private float cameraZoomSpeed = 5f; // The zoom level of the camera
    private Vector3 _currentVelocity = Vector3.zero; // The velocity of the camera used by SmoothDamp


    private Vector3 _targetPosition;

    private bool isRoomAlchemy = true;

    // Update is called once per frame
    void Update()
    {
        if (isRoomAlchemy)
        {
            // Calculate the desired camera position based on the player's position
            _targetPosition = new Vector3(Mathf.Clamp(player.position.x, -0.76f, 0.33f), 19.98f, transform.position.z);
            // Check if the camera needs to move
            if (Mathf.Abs(_targetPosition.x - transform.position.x) > 0.1f)
            {
                MoveCamera();
            }
        }

        TeleportCamera();

    }

    // Move the camera smoothly towards the target position
    void MoveCamera()
    {

        // SmoothDamp will gradually move the camera's position towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, cameraDrag * Time.deltaTime);
    }

    public void TeleportCamera()
    {
        if (!isRoomAlchemy)
        {
            Vector3 targetPosition = new Vector3(-19.7f, 18.77f, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, cameraDrag * Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, cameraZoomSpeed * Time.deltaTime);
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 6f, cameraZoomSpeed * Time.deltaTime);
        }

    }


    public void SetAlchemyRoom(bool value)
    {
        isRoomAlchemy = value;
    }

    public bool GetAlchemyRoom()
    {
        return isRoomAlchemy;
    }
}