using UnityEngine;

public class Recipes : MonoBehaviour
{
    [SerializeField] private CustomerManager cm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Potion[] potions = cm.GetPotions();
        foreach (Potion potion in potions)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
