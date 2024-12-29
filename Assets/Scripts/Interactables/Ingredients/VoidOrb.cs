using UnityEngine;

public class VoidOrb : Ingredient
{
    [SerializeField] Material defaultDarkerMaterial;
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
            materials[0] = highlightMaterial;
            materials[1] = highlightMaterial;
            Debug.Log("Highlighting");
        }
        else
        {
            materials[0] = defaultMaterial;
            materials[1] = defaultDarkerMaterial;
        }

        renderer.materials = materials;
    }
}
