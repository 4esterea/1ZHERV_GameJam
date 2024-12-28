using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Vector3 interactOffset;
    [SerializeField] private LayerMask interactLayer;
    private HashSet<Ingridient> highlightedIngridients = new HashSet<Ingridient>();
    // Update is called once per frame
    void Update()
    {
        Highlight();
        //Interact();
    }
    
    
    private void Highlight()
    {
        Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
        Quaternion rotation = transform.rotation;
        Collider[] colliders = Physics.OverlapBox(forwardOffset, Vector3.one / 2, rotation, interactLayer);

        Ingridient closestIngridient = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            Ingridient ingridient = collider.GetComponent<Ingridient>();
            if (ingridient != null)
            {
                float distance = Vector3.Distance(transform.position, ingridient.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIngridient = ingridient;
                }
            }
        }

        foreach (Ingridient ingridient in highlightedIngridients)
        {
            ingridient.SetHighlight(false);
        }

        highlightedIngridients.Clear();

        if (closestIngridient != null)
        {
            closestIngridient.SetHighlight(true);
            highlightedIngridients.Add(closestIngridient);
        }
    }
    
    
    // void Interact()
    // {
    //     if (_input.Player.Interact.triggered )
    //     {
    //         
    //     }
    // }
    
    private void OnDrawGizmos()
         {
             Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
             Quaternion rotation = transform.rotation;
             Gizmos.matrix = Matrix4x4.TRS(forwardOffset, rotation, Vector3.one);
             Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
         }
         
}
