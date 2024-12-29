using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    protected bool _isHeld;
    protected bool _wasTossed = false;
    protected bool _wasHeld = false;
    protected Rigidbody _rb;
    protected Transform _player;
    protected bool _isInCrate = true;
    [SerializeField] protected Vector3 holdingOffset;
    Cauldron _cauldron;
    private MeshCollider _collider;

    protected bool _isHighlighted;
    [SerializeField] protected Material highlightMaterial;
    [SerializeField] protected Material defaultMaterial;

    private void OnEnable()
    {
        _player = GameObject.Find("PlayerCapsule").transform;
        _rb = GetComponent<Rigidbody>();
        _cauldron = GameObject.Find("Cauldron").GetComponent<Cauldron>();
        _collider = GetComponent<MeshCollider>();
        _rb.isKinematic = true;
        _collider.enabled = false;
    }
    

    protected virtual void Update()
    {
        Interaction();
    }

    protected virtual void Highlight()
    {
       
    }
    
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cauldron"))
        {
            Debug.Log("Collision detected with " + collision.gameObject.name);
            Debug.Log("Ingredient " + this.name);
            _cauldron.PutIngredient(this);
        }
    }
    
    public void Interaction()
    {
        if (_isInCrate)
        {
            return;
        }
        if (_isHeld)
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
            transform.position = _player.transform.position + holdingOffset + _player.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(_player.transform.forward, Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            _collider.enabled = true;
            _rb.isKinematic = false;
            if (!_wasTossed && _wasHeld)
            {
                _rb.AddForce((transform.forward + Vector3.up*2) * 6f, ForceMode.Impulse);
                _wasTossed = true;
                _wasHeld = false;
            }
        }
    }
    
    public void SetHighlight(bool value)
    {
        _isHighlighted = value;
    }
    
    public void SetHeld(bool value)
    {
        _isHeld = value;
    }
    
    public void SetWasHeld(bool value)
    {
        _wasHeld = value;
    }
    public void SetTossed(bool value)
    {
        _wasTossed = value;
    }
    
    public void SetInCrate(bool value)
    {
        _isInCrate = value;
    }
}
