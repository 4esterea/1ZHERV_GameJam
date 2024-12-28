using System;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    private bool _isHeld;
    private bool _isHighlighted;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Rigidbody _rb;
    private SphereCollider _collider;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 holdingOffset;

    private void OnEnable()
    {
        _collider = GetComponent<SphereCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Highlight();
        if (_isHeld)
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
            transform.position = player.transform.position + holdingOffset + player.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(player.transform.forward, Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            _collider.enabled = true;
            _rb.isKinematic = false;
        }
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
}
