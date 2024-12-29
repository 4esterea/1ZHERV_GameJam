using UnityEngine;

public class ExplodingCrystal : Ingredient
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Highlight();
    }

    protected override void Highlight()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        if (_isHighlighted)
        {
            materials[1] = highlightMaterial;
        }
        else
        {
            materials[1] = defaultMaterial;
        }

        renderer.materials = materials;
    }
}
