using System;
using System.Linq;
using UnityEngine;

public class Crate : Interactable
{
    private int _containsCounter = 5;
    private bool _wasTaken = false;
    private bool _isHeld = false;
    private Collider _collider;
    private Rigidbody _rb;
    private PlayerInteract _player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    private void OnEnable()
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
