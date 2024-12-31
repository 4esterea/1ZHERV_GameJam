using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float chaosLevel = 0f;
    private float maxChaosLevel = 10f;
    [SerializeField] private float chaosDecreaseRate = 0.1f;
    [SerializeField] private Slider chaosSlider;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public void IncreaseChaosLevel(float amount)
    {
        chaosLevel += amount;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (chaosLevel > 0)
        {
            chaosLevel -= chaosDecreaseRate * Time.deltaTime;
        }
        chaosSlider.value = chaosLevel;
        if (chaosLevel >= maxChaosLevel)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Uh oh! The chaos level is too high!\n You've successfully caused chaos for " + Math.Round(Time.time) + " seconds!";
            // Pause the game
            Time.timeScale = 0;
        }
        timerText.text = "Time: " + Math.Round(Time.time);
    }
}
