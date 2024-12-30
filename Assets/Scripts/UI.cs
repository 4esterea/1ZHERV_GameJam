using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] CustomerManager _cm;
    [SerializeField] RawImage rawImage;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Camera redCamera;
    [SerializeField] Camera yellowCamera;
    [SerializeField] Camera greenCamera;
    [SerializeField] Camera blueCamera;
    private RenderTexture redRenderTexture;
    private RenderTexture yellowRenderTexture;
    private RenderTexture greenRenderTexture;
    private RenderTexture blueRenderTexture;
    private Potion desiredPotion;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        redRenderTexture = new RenderTexture(100, 100, 24);
        yellowRenderTexture = new RenderTexture(100, 100, 24);
        greenRenderTexture = new RenderTexture(100, 100, 24);
        blueRenderTexture = new RenderTexture(100, 100, 24);
        
        redCamera.targetTexture = redRenderTexture;
        yellowCamera.targetTexture = yellowRenderTexture;
        greenCamera.targetTexture = greenRenderTexture;
        blueCamera.targetTexture = blueRenderTexture;
        
        // Create a RawImage and set its texture to the Render Texture
        
    }

    // Update is called once per frame
    
    public void ClearPotionDisplay()
    {
        rawImage.texture = null;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0);
        text.text = "";
    }
    
    public void UpdatePotionDisplay()
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 1);
        Customer customer = _cm.GetFirst();
        if (customer != null)
        {
            desiredPotion = customer.GetDesiredPotion();
        } else {
            desiredPotion = null;
        }
        if (desiredPotion == null)
        {
            rawImage.texture = null;
            return;
        }
        switch (desiredPotion.name)
        {
            case "RedPotion":
                rawImage.texture = redRenderTexture;
                text.text = "Red Potion";
                break; 
            case "YellowPotion":
                rawImage.texture = yellowRenderTexture;
                text.text = "Yellow Potion";
                break;
            case "GreenPotion":
                rawImage.texture = greenRenderTexture;
                text.text = "Green Potion";
                break;
            case "BluePotion":
                rawImage.texture = blueRenderTexture;
                text.text = "Blue Potion";
                break;
            default:
                break;
        }
    }
}
