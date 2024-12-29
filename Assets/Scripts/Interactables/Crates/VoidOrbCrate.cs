using Unity.VisualScripting;
using UnityEngine;

public class VoidOrbCrate : Crate
{
    [SerializeField] Ingredient voidOrb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }
    
    public override Ingredient GrabIngredient()
    {
        if (_containsCounter > 0)
        {
            _containsCounter--;
            Destroy(transform.GetChild(_containsCounter).GameObject());
            return Instantiate(voidOrb);
        }
        else
        {
            Debug.Log("No ingredients in the crate");
            return null;
        }
    }
}
