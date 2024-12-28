using System;
using UnityEngine;

public class Cauldron : Interactable
{
    private Ingredient[] _contains = new Ingredient[4];
    [SerializeField] private PlayerInteract playerInteract; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // private void Awake()
    // {
    //     _playerInteract = FindFirstObjectByType<PlayerInteract>();
    // }

    public void PutIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < _contains.Length; i++)
        {
            if (_contains[i] == null)
            {
                _contains[i] = ingredient;
                ingredient.transform.position = transform.position + Vector3.up * 2f;
                ingredient.SetHeld(false);
                ingredient.Interaction();
                playerInteract.SetHolding(false);
                playerInteract.SetHeldIngredient(null);
                return;
            }
            Debug.Log(_contains[i].name);
        }
        Debug.Log("MAMA");
    }
    
    public override void Interact()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
