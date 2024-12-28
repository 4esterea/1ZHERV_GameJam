using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // The player's Transform
    [SerializeField] private float cameraDrag = 10f; // The drag/smoothness of the camera movement
    private Vector3 _currentVelocity = Vector3.zero; // The velocity of the camera used by SmoothDamp

    private Vector3 _targetPosition;

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired camera position based on the player's position
        _targetPosition = new Vector3(Mathf.Clamp(player.position.x, 0.7173205f, 12.7538f), transform.position.y, transform.position.z);
        // Check if the camera needs to move
        if (Mathf.Abs(_targetPosition.x - transform.position.x) > 0.1f) 
        {
            MoveCamera();
        }
    }

    // Move the camera smoothly towards the target position
    void MoveCamera()
    {
        // SmoothDamp will gradually move the camera's position towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, cameraDrag * Time.deltaTime);
    }
}