using System;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    private bool _isHeld;
    private bool _isHighlighted;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private void Update()
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
}
