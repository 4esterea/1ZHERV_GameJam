using UnityEngine;

public class Potion : MonoBehaviour
{
    bool isPotionReady = true;
    bool isBeingHeld = false;
    private CapsuleCollider _collider;
    private Rigidbody _rb;
    private bool _wasTossed = false;
    [SerializeField] private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPotionReady && !isBeingHeld)
        {
            transform.Rotate(new Vector3(0, .15f, .1f), .5f);
        }
        else if (!isPotionReady && isBeingHeld)
        {
            _collider.enabled = false;
            transform.position = player.transform.position + player.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(player.transform.forward, Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            _collider.enabled = true;
            _rb.isKinematic = false;
            if (!_wasTossed)
            {
                _rb.AddForce((transform.forward + Vector3.up) * 6f, ForceMode.Impulse);
                _wasTossed = true;
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Handle collision
            Debug.Log("Collision detected with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
    
    public void SetHeld(bool ready)
    {
        isBeingHeld = ready;
    }
    
    public void SetPotionReady(bool ready)
    {
        isPotionReady = ready;
    }
    
    public Rigidbody GetRigidbody()
    {
        return _rb;
    }
}
