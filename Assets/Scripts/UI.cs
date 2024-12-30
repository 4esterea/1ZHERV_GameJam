using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] RawImage rawImage;
    [SerializeField] Camera redCamera;
    [SerializeField] Camera yellowCamera;
    [SerializeField] Camera greenCamera;
    [SerializeField] Camera blueCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderTexture redRenderTexture = new RenderTexture(100, 100, 24);
        RenderTexture yellowRenderTexture = new RenderTexture(100, 100, 24);
        RenderTexture greenRenderTexture = new RenderTexture(100, 100, 24);
        RenderTexture blueRenderTexture = new RenderTexture(100, 100, 24);
        
        redCamera.targetTexture = redRenderTexture;
        // Create a RawImage and set its texture to the Render Texture
        rawImage.texture = redRenderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
