using Unity.VisualScripting;
using UnityEngine;

public class FragileCrystalCrate : Crate
{
    [SerializeField] Ingredient fragileCrystal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void OnEnable()
    {
        _containsCounter = 5;
        base.OnEnable();
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
            return Instantiate(fragileCrystal);
        }
        else
        {
            Debug.Log("No ingredients in the crate");
            return null;
        }
    }
}
