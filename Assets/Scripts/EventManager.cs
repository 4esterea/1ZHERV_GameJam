using UnityEngine;
using System.Collections;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private RecipeBook rb;
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private float fadeSpeed = 0f;
    private bool _isInverted = false;
    private bool _isRerolled = false;
    private float _alpha = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    enum Events 
    {
        InvertControls,
        RerollRecipes
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 1f) < 0.0003f && !_isInverted) // 1% chance per frame
        {
            eventText.text = "Controls Inverted!";
            _alpha = 1f;
            StartCoroutine(InvertControlsEventCoroutine());
        }
        if (Random.Range(0f, 1f) < 0.0003f && !_isRerolled) // 1% chance per frame
        {
            eventText.text = "Recipes Rerolled!";
            _alpha = 1f;
            StartCoroutine(RerollEventCoroutine());
        }
        if (_alpha > 0)
        {
            _alpha -= fadeSpeed * Time.deltaTime;
            eventText.color = new Color(255, 255, 255, _alpha);
        }
    }

    private IEnumerator InvertControlsEventCoroutine()
    {
        _isInverted = true;
        Debug.Log("Invert Event started");
        pm.SetControlsInverted(true);
        yield return new WaitForSeconds(10f); // 10-second duration
        Debug.Log("Invert Event ended");
        pm.SetControlsInverted(false);
        StartCoroutine(CooldownEventCoroutine(Events.InvertControls));
    }

    private IEnumerator RerollEventCoroutine()
    {
        _isRerolled = true;
        Debug.Log("Reroll Event started");
        rb.RerollAllRecipes();
        yield return new WaitForSeconds(20f);
        StartCoroutine(CooldownEventCoroutine(Events.RerollRecipes));
    }

    private IEnumerator CooldownEventCoroutine(Events eventx)
    {
        yield return new WaitForSeconds(5f); // 5-second cooldown

        switch(eventx)
        {
            case Events.InvertControls:
                // Handle InvertControls event (e.g., toggle control inversion)
                Debug.Log("Cooldown ended for InvertControls event");
                _isInverted = false; // Example action: set controls to inverted state
                break;

            case Events.RerollRecipes:
                // Handle RerollRecipes event (e.g., reroll recipe logic)
                Debug.Log("Cooldown ended for RerollRecipes event");
                _isRerolled = false; // Example action: set reroll state to true
                break;
            default:
                break;
        }
    }

}