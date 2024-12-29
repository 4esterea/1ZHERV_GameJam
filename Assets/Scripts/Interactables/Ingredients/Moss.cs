using UnityEngine;

public class Moss : Ingredient
{

    // Update is called once per frame
    protected override void Update()
    {
        Highlight();
        base.Update();
    }

    protected override void Highlight()
    {

        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        if (_isHighlighted)
        {
            materials[0] = highlightMaterial;
            materials[1] = highlightMaterial;
        }
        else
        {
            materials[0] = defaultMaterial;
            materials[1] = defaultMaterial;
        }

        renderer.materials = materials;
    }
}
