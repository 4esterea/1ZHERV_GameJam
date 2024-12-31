using UnityEngine;
using UnityEngine.Rendering;

public class CrateButton : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject IngredientOrderPanel;
    private bool panelIsShown = false;
    [SerializeField] private GameObject Player;
    void Start()
    {
        
    }
    
    public override void Interact()
    {
        if (panelIsShown)
        {
            IngredientOrderPanel.SetActive(false);
            panelIsShown = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            IngredientOrderPanel.SetActive(true);
            panelIsShown = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance > 2)
        {
            IngredientOrderPanel.SetActive(false);
            panelIsShown = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}