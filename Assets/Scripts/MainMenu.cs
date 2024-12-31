using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        // Add listeners to buttons
        startButton.GetComponent<Button>().onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene("Scene"));
        #if UNITY_EDITOR
            // Quitting in Unity Editor:
            quitButton.GetComponent<Button>().onClick.AddListener(() => QuitGame());
        #elif UNITY_WEBGL
            // Quitting in the WebGL build:
            // Application.OpenURL(Application.absoluteURL);
        #else
            // Quitting in all other builds:
            quitButton.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        #endif
        
        
    }
    
    void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
