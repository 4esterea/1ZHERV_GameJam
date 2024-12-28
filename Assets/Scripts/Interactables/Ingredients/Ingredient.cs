using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    protected bool _isHeld;
    protected Rigidbody _rb;
    protected Transform _player;
    [SerializeField] protected Vector3 holdingOffset;

    private bool _isHighlighted;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private void OnEnable()
    {
        _player = GameObject.Find("Capsule").transform;
        Debug.Log("Player " + _player);
        _rb = GetComponent<Rigidbody>();
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
    
    public void SetHighlight(bool value)
    {
        _isHighlighted = value;
    }
    
    public void SetHeld(bool value)
    {
        _isHeld = value;
    }

    public virtual void Interaction(){}
}
