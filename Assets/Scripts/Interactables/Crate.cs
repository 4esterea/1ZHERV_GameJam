using UnityEngine;

public class Crate : Interactable
{
    private Ingredient[] _contains = new Ingredient[5];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Interact()
    {
         
    }
    
    public Ingredient GrabIngredient()
    {
        for (int i = 4; i > -1; i--)
        {
            if (_contains[i] != null)
            {
                return _contains[i];
            }
        }
        Debug.Log("No ingredients in the crate");
        return null;
    }
}
