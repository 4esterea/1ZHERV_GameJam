using System;
using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : Interactable
{
    private Ingredient[] _contains = new Ingredient[4];
    [SerializeField] private PlayerInteract playerInteract; 
    private bool potionReady = false;
    [SerializeField] private GameObject redPotionPrefab;
    [SerializeField] private GameObject yellowPotionPrefab;
    [SerializeField] private GameObject greenPotionPrefab;
    [SerializeField] private GameObject bluePotionPrefab;
    private Potion _potion;
    [SerializeField] private RecipeBook recipeBookObj; 
    [SerializeField] private float explosionForce = 1000f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float upwardsModifier = 1f;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private LayerMask playerLayerMask;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PutIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < _contains.Length; i++)
        {
            if (_contains[i] == null)
            {
                ingredient.gameObject.name = ingredient.name.Replace("(Clone)", "").Trim();
                _contains[i] = ingredient;
                Debug.Log(_contains[i].name);

                ingredient.transform.position = transform.position*100 + Vector3.up * 2f;
                ingredient.gameObject.SetActive(false);

                return;
            }
        }
    }

    public void Cook()
    {
        if (potionReady || _contains[0] == null)
        {
            return;
        }
        Debug.Log("Cooking");    
        Recipe[] recipeBook = recipeBookObj.GetRecipeBook();
        
        // Iterate through the contains and compare to the recipe book 
        foreach (Recipe recipe in recipeBook)
        {
            Debug.Log("Checking for " + recipe.GetPotion().name);
            bool match = true;
            for (int i = 0; i < 4; i++)
            {
                Ingredient recipeIngredient = recipe.GetIngredients()[i];
                Ingredient containsIngredient = _contains[i];
                Debug.Log((recipeIngredient == null ? "null" : recipe.GetIngredients()[i].name) + " c= " + (_contains[i] == null ? "null" : _contains[i].name));
                if (recipeIngredient == null && containsIngredient == null)
                {
                    continue;
                } 
                if (recipeIngredient == null && containsIngredient != null || 
                    recipeIngredient != null && containsIngredient == null)
                {
                    match = false;
                    break;
                }
                if (recipeIngredient.name != containsIngredient.name)
                {
                    match = false;
                }
            }
            if (match)
            {
                Debug.Log("Match found");
                potionReady = true;
                _potion = SpawnPotion(recipe.GetPotion()).GetComponent<Potion>();
                break;
            }
        }
        
        foreach (var ingredient in _contains)
        {
            if (ingredient != null)
            {
                Destroy(ingredient.gameObject);
            }
        } 
        if (!potionReady)
        {
            Explode();
        }
    }
    
    GameObject SpawnPotion(Potion potion)
    {
        Debug.Log("Potion spawned");
        switch (potion.name)
        {
            case "RedPotion":
                return Instantiate(redPotionPrefab, transform.position + Vector3.up * 2f, Quaternion.Euler(30f, 0, 0));
            case "YellowPotion":
                return Instantiate(yellowPotionPrefab, transform.position + Vector3.up * 2f, Quaternion.Euler(30f, 0, 0));
            case "GreenPotion":
                return Instantiate(greenPotionPrefab, transform.position + Vector3.up * 2f, Quaternion.Euler(30f, 0, 0));
            case "BluePotion":
                return Instantiate(bluePotionPrefab, transform.position + Vector3.up * 2f, Quaternion.Euler(30f, 0, 0));
            default:
                return null;
        }
    }

    void Explode()
    {
        explosionLayerMask = explosionLayerMask | playerLayerMask;
        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply explosion force
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
    
    public bool IsFull()
    {
        if (_contains[3] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
