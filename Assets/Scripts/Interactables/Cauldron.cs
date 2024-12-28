using System;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : Interactable
{
    private Ingredient[] _contains = new Ingredient[4];
    [SerializeField] private PlayerInteract playerInteract; 
    private bool potionReady = false;
    [SerializeField] private GameObject potionPrefab;
    private Potion _potion;
    
    
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
                ingredient.transform.position = transform.position*100 + Vector3.up * 2f;
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

    public void Cook()
    {
        if (potionReady || _contains[0] == null)
        {
            return;
        }
        Debug.Log("Cooking");    
        foreach (var ingredient in _contains)
        {
            if (ingredient != null)
            {
                Destroy(ingredient.gameObject);
            }
        } 
        potionReady = true;
        _potion = SpawnPotion().GetComponent<Potion>();
    }
    
    GameObject SpawnPotion()
    {
        Debug.Log("Potion spawned");
        return Instantiate(potionPrefab, transform.position + Vector3.up * 2f, Quaternion.Euler(30f, 0, 0));
    }
    public override void Interact()
    {
        
    }
    
    public bool isReady()
    {
        return potionReady;
    }
    
    public Potion GetPotion()
    {
        return _potion;
    }
    
    public void DeletePotion()
    {
        potionReady = false;
        _potion = null;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
