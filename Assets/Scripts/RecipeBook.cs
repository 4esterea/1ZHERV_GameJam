using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] private CustomerManager cm; 
    private Recipe[] _recipeBook = new Recipe[4];
    [SerializeField] private Ingredient[] ingredients;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
    Potion[] potions = cm.GetPotions();
    int index = 0;
    foreach (Potion potion in potions)
    {
        Recipe recipe = gameObject.AddComponent<Recipe>();
        recipe.Initialize(potion);
        Random.InitState(Guid.NewGuid().GetHashCode());
        int ingredientCount = Random.Range(1, 4);
        Debug.Log(potion + " : " + ingredientCount + " ingredient(s):");
        for (int i = 0; i < ingredientCount; i++)
        {
            recipe.AddIngredient(ingredients[Random.Range(0, 6)]);
        }
        _recipeBook[index++] = recipe;
        StartCoroutine(WaitCoroutine());
    }
}

    
public Recipe[] GetRecipeBook()
{
    return _recipeBook;
}
private IEnumerator WaitCoroutine()
{
    yield return new WaitForSeconds(0.0003f); // 300 nanoseconds
}
}
