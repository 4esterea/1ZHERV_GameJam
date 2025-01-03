using System;
using System.Linq;
using UnityEngine;

public class Crate : Interactable
{
    protected int _containsCounter = 5;
    [SerializeField] protected bool _wasTaken = false;
    protected bool _isHeld = false;
    protected Collider _collider;
    protected Rigidbody _rb;
    protected PlayerInteract _player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        Interact();
    }

    protected void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindFirstObjectByType<PlayerInteract>();
        _collider = GetComponent<MeshCollider>();
    }

    public override void Interact()
    {
        if (_isHeld)
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
            transform.position = _player.transform.position + _player.transform.forward * 1.5f + Vector3.up * .7f;
            Quaternion rotation = Quaternion.LookRotation(_player.transform.forward, Vector3.up) * Quaternion.Euler(0, 90, 0);
            transform.rotation = rotation;
        }
        else
        {
            _rb.isKinematic = false;
            _collider.enabled = true;
        }
        
    }
    
     public virtual Ingredient GrabIngredient()
    {
        // if (_containsCounter > 0)
        // {
        //     _containsCounter--;
        //     return Instantiate(FindObjectOfType<Ingredient>());
        // }
        // Debug.Log("No ingredients in the crate");
        return null;
    }
    
    public bool WasTaken()
    {
        return _wasTaken;
    }
    
    public void SetWasTaken(bool wasTaken)
    {
        _wasTaken = wasTaken;
    }
    
    public void SetIsHeld(bool isHeld)
    {
        _isHeld = isHeld;
    }
}
