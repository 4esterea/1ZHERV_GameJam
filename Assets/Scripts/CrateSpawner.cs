using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrateSpawner : MonoBehaviour
{
    [SerializeField] private Crate[] cratePrefabs;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Slider crateSlider;
    public bool isSpawning = false;

    void Start()
    {
        // Add listeners to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(SpawnCrateWithDelay(index)));
        }
    }

    private IEnumerator SpawnCrateWithDelay(int index)
{
    crateSlider.gameObject.SetActive(true);
    if (isSpawning)
    {
        yield break;
    }
    isSpawning = true;
    float delay = 10f;
    float elapsedTime = 0f;

    while (elapsedTime < delay)
    {
        yield return null; // Wait for the next frame
        elapsedTime += Time.deltaTime;
        float remainingTime = delay - elapsedTime;
        crateSlider.value = 10 - remainingTime;
        Debug.Log("Time remaining: " + remainingTime + " seconds");
    }

    SpawnCrate(index);
    isSpawning = false;
    crateSlider.gameObject.SetActive(false);
}

    public void SpawnCrate(int index)
    {
        if (index >= 0 && index < cratePrefabs.Length)
        {
            Instantiate(cratePrefabs[index], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Index out of range for cratePrefabs");
        }
    }

    void Update()
    {
        // Update logic if needed
    }
}