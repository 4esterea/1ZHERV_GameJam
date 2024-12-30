using TMPro;
using UnityEngine;

public class RecipeBookUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI redPotionRecipeText;
    [SerializeField] TextMeshProUGUI yellowPotionRecipeText;
    [SerializeField] TextMeshProUGUI greenPotionRecipeText;
    [SerializeField] TextMeshProUGUI bluePotionRecipeText;
    void Start()
    {
        
    }
    public void SetRecipes(Potion potion, string recipe)
    {
        switch (potion.name)
        {
            case "RedPotion":
                redPotionRecipeText.text = recipe;
                break;
            case "YellowPotion":
                yellowPotionRecipeText.text = recipe;
                break;
            case "GreenPotion":
                greenPotionRecipeText.text = recipe;
                break;
            case "BluePotion":
                bluePotionRecipeText.text = recipe;
                break;
            default:
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
