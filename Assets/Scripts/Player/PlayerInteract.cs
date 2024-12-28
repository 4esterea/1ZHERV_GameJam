using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Input _input;
    [SerializeField] private Vector3 interactOffset;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private bool isHolding;
    private Ingridient _heldIngridient;
    private HashSet<Ingridient> _highlightedIngridients = new HashSet<Ingridient>();

    private void Awake()
    {
        _input = new Input();
    }

    private void OnEnable()
    {
        if (!_input.Player.enabled)
        {
            _input.Player.Enable();
        }

        _input.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        if (_input.Player.enabled)
        {
            _input.Player.Disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Highlight();
    }
    
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isHolding)
        {
            isHolding = false;
            Interact(_heldIngridient);
            _heldIngridient = null;
        }
        else if (_highlightedIngridients.Count > 0)
        {
            isHolding = true;
            _heldIngridient = _highlightedIngridients.First();
            Interact(_heldIngridient);
        }
    }
    
    private void Highlight()
    {
        Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
        Quaternion rotation = transform.rotation;
        Collider[] colliders = Physics.OverlapBox(forwardOffset, Vector3.one / 2, rotation, interactLayer);

        Ingridient closestIngridient = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            Ingridient ingridient = collider.GetComponent<Ingridient>();
            if (ingridient != null)
            {
                float distance = Vector3.Distance(transform.position, ingridient.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIngridient = ingridient;
                }
            }
        }

        foreach (Ingridient ingridient in _highlightedIngridients)
        {
            ingridient.SetHighlight(false);
        }

        _highlightedIngridients.Clear();

        if (closestIngridient != null)
        {
            closestIngridient.SetHighlight(true);
            _highlightedIngridients.Add(closestIngridient);
        }
    }
    
    
    void Interact(Ingridient ingridient)
    {
        if (isHolding)
        {
            ingridient.SetHeld(true);
        }
        else
        {
            ingridient.SetHeld(false);
        }
    }
    
    private void OnDrawGizmos()
         {
             Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
             Quaternion rotation = transform.rotation;
             Gizmos.matrix = Matrix4x4.TRS(forwardOffset, rotation, Vector3.one);
             Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
         }
         
}
