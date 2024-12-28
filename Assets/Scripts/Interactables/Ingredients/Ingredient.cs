using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    protected bool _isHeld;
    protected bool _wasTossed = false;
    protected Rigidbody _rb;
    protected Transform _player;
    [SerializeField] protected Vector3 holdingOffset;
    Cauldron _cauldron;

    private bool _isHighlighted;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private void OnEnable()
    {
        _player = GameObject.Find("PlayerCapsule").transform;
        Debug.Log("Player " + _player);
        _rb = GetComponent<Rigidbody>();
        _cauldron = GameObject.Find("Cauldron").GetComponent<Cauldron>();
    }

    protected virtual void Update()
    {
        Highlight();
    }

    void Highlight()
    {
        if (_isHighlighted)
        {
            GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = defaultMaterial;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cauldron"))
        {
            Debug.Log("Collision detected with " + collision.gameObject.name);
            _cauldron.PutIngredient(this);
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
    
    public void SetTossed(bool value)
    {
        _wasTossed = value;
    }
    public virtual void Interaction(){}
}
