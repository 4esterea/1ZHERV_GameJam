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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
