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
    private Ingredient _heldIngredient;
    [SerializeField] private Interactable interactable;
    private HashSet<Ingredient> _highlightedIngridients = new HashSet<Ingredient>();

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
        Cauldron cauldron = interactable as Cauldron;
        if (isHolding && interactable == cauldron && cauldron != null)
        {
            cauldron.PutIngredient(_heldIngredient);
        }
        else if (isHolding)
        {
            isHolding = false;
            Interact(_heldIngredient);
            _heldIngredient = null;
        }
        else if (_highlightedIngridients.Count > 0)
        {
            isHolding = true;
            _heldIngredient = _highlightedIngridients.First();
            Interact(_heldIngredient);
        }
    }
    
    private void Highlight()
    {
        Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
        Quaternion rotation = transform.rotation;
        Collider[] colliders = Physics.OverlapBox(forwardOffset, Vector3.one / 2, rotation, interactLayer);
        
        interactable = null;
        Ingredient closestIngredient = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            Ingredient ingredient = collider.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                float distance = Vector3.Distance(transform.position, ingredient.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIngredient = ingredient;
                }
            }
            interactable = collider.GetComponent<Interactable>();
        }

        foreach (Ingredient ingredient in _highlightedIngridients)
        {
            ingredient.SetHighlight(false);
        }

        _highlightedIngridients.Clear();

        if (closestIngredient != null)
        {
            closestIngredient.SetHighlight(true);
            _highlightedIngridients.Add(closestIngredient);
        }
    }
    
    
    void Interact(Ingredient ingredient)
    {
        if (isHolding)
        {
            ingredient.SetHeld(true);
        }
        else
        {
            ingredient.SetHeld(false);
        }
    }
    
    public void SetHolding(bool holding)
    {
        isHolding = holding;
    }
    
    public void SetHeldIngredient(Ingredient ingredient)
    {
        _heldIngredient = ingredient;
    }
    private void OnDrawGizmos()
         {
             Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
             Quaternion rotation = transform.rotation;
             Gizmos.matrix = Matrix4x4.TRS(forwardOffset, rotation, Vector3.one);
             Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
         }
         
}
