using System;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    Potion _potion;
    Ingredient[] _ingredients = new Ingredient[4];

    void Start()
    {

    }

    public void Initialize(Potion potion)
    {
        this._potion = potion;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < _ingredients.Length; i++)
        {
            if (_ingredients[i] == null)
            {
                _ingredients[i] = ingredient;
                Debug.Log(ingredient.name);
                break;
            }
        }
    }
    
    public Ingredient[] GetIngredients()
    {
        return _ingredients;
    }
    
    
    public Potion GetPotion()
    {
        return _potion;
    }
    void Update()
    {

    }
}