using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] private CustomerManager cm;
    [SerializeField] private Ingredient[] ingredients;
    private Recipe[] _recipeBook = new Recipe[4];
    private HashSet<string> uniqueRecipes = new HashSet<string>();
    [SerializeField] private RecipeBookUI recipeBookUI;

    void Start()
    {
        RerollAllRecipes();
    }

    private Recipe RerollRecipe(Potion potion)
    {
        while (true)
        {
            Recipe recipe = gameObject.AddComponent<Recipe>();
            recipe.Initialize(potion);
            Random.InitState(Guid.NewGuid().GetHashCode());
            int ingredientCount = Random.Range(2, 5);

            List<string> ingredientNames = new List<string>();
            for (int i = 0; i < ingredientCount; i++)
            {
                Ingredient ingredient = ingredients[Random.Range(0, ingredients.Length)];
                ingredientNames.Add(ingredient.name);
                recipe.AddIngredient(ingredient);
            }
            
            string recipeKey = string.Join(" , ", ingredientNames);

            if (!uniqueRecipes.Contains(recipeKey))
            {
                uniqueRecipes.Add(recipeKey);
                Debug.Log(potion + " : " + ingredientCount + " ingredient(s): " + recipeKey);
                recipeBookUI.SetRecipes(potion, recipeKey);
                return recipe;
            }
            else
            {
                Destroy(recipe);
                Debug.Log("Duplicate recipe found, rerolling...");
            }
        }
    }
    
    public void RerollAllRecipes()
    {
        Potion[] potions = cm.GetPotions();
        int index = 0;
        foreach (Potion potion in potions)
        {
            Recipe recipe = RerollRecipe(potion);
            _recipeBook[index++] = recipe;

            if (index >= _recipeBook.Length)
            {
                break;
            }
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