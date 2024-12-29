using UnityEngine;

public class FragileCrystal : Ingredient
{
    new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Cauldron"))
        {
            // Handle collision
            Debug.Log("Collision detected with on FC destroy " + collision.gameObject.name);
            // Destroy(transform.parent.gameObject);
            // Destroy(gameObject);
        }
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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Highlight();
    }
    
}
