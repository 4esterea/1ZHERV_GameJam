using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    // Customer prefab
    private GameObject customerPrefab;
    // Desired potion
    private Potion desiredPotion;
    // Target position
    private Vector3 targetPosition;
    private bool wasServed = false;
    private bool isUnsatisfied = false;
    private bool isSatisfied = false;
    private bool isWaiting = false;
    private bool isFirst = false;
    private bool wasCounterDecremented = false;
    [SerializeField] private bool isBeingServed = false;
    // Customer patience timer
    [SerializeField] private float patienceTimer;
    // Customer satisfaction timer
    [SerializeField] private float satisfactionTimer;
    // Customer Manager reference
    private CustomerManager customerManager;
    private TextMeshProUGUI text;
    private UI ui;
    private Slider satisfactionSlider;
    private Slider patienceSlider;
    [SerializeField] private GameObject patienceSliderPrefab;
    private GameManager gameManager;
    
    public Customer(GameObject prefab, Potion potion)
    {
        customerPrefab = prefab;
        desiredPotion = potion;
    }
    

    private void Start()
    {
        
        satisfactionSlider = GameObject.Find("Canvas").transform.Find("SatisfactionSlider").GetComponent<Slider>();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        customerManager = GameObject.Find("CustomerManager").GetComponent<CustomerManager>();
        text = GameObject.Find("Canvas").transform.Find("PotionDisplayText").GetComponent<TextMeshProUGUI>();
        ui = GameObject.Find("Canvas").GetComponent<UI>();
    }

    private void Update()
    {

        
        if (isUnsatisfied)
        {
            if (!wasCounterDecremented)
            {
                customerManager.DecrementCounter();
                wasCounterDecremented = true;
                
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition - new Vector3(15f, 0, -15f), 5f * Time.deltaTime);
            if (transform.position == targetPosition - new Vector3(15f, 0, -15f))
            {
                Destroy(gameObject);
            }
        }
        else if (isSatisfied)
        {
            if (!wasCounterDecremented)
            {
                customerManager.DecrementCounter();
                wasCounterDecremented = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition - new Vector3(0, 0, 5f), 5f * Time.deltaTime);
            if (transform.position == targetPosition - new Vector3(0, 0, 5f))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Move the customer to the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            
        }

        if (isWaiting && patienceSlider != null)
        {
            patienceSlider.transform.position = transform.position + new Vector3(0, 3f, 0);
        }
        else if (patienceSlider != null)
        {
            Destroy(patienceSlider.gameObject);
        }
        
        // If the customer is not being served and is not waiting
        if (!isBeingServed && !isWaiting && transform.position == targetPosition)
        {
            // Start the patience timer
            patienceSlider = Instantiate(patienceSliderPrefab, transform.position + new Vector3(0, 3f, 0), Quaternion.Euler(45, 0, 0)).GetComponent<Slider>();
            patienceSlider.transform.SetParent(GameObject.Find("Canvas").transform);
            StartCoroutine(PatienceCoroutine());
            isWaiting = true;
        }
        
        
        if (!isBeingServed && transform.position == targetPosition && isFirst)
        {

            satisfactionSlider.transform.position = transform.position + new Vector3(0, 3f, 0);
            satisfactionSlider.gameObject.SetActive(true);
            isWaiting = false;
            isBeingServed = true;
            ui.UpdatePotionDisplay();
            StartCoroutine(SatisfactionCoroutine());
        }

    }
    
    private IEnumerator SatisfactionCoroutine()
    {
        float _satisfactionTimer = satisfactionTimer;
        satisfactionSlider.maxValue = satisfactionTimer;


        // Start timer until it reaches 0
        while (_satisfactionTimer > 0)
        {
            // If player gets satisfied - stop the timer
            if (isSatisfied)
            {
                if (satisfactionSlider != null)
                {
                    satisfactionSlider.gameObject.SetActive(false);
                }
                yield break;
            }
            _satisfactionTimer -= Time.deltaTime;
            satisfactionSlider.value = _satisfactionTimer;
            yield return null;
        }
        
        // If the timer reaches 0
        isUnsatisfied = true;
        satisfactionSlider.gameObject.SetActive(false);
        gameManager.IncreaseChaosLevel(3f);
        ui.ClearPotionDisplay();
    }
        
    private IEnumerator PatienceCoroutine()
    {
        float _patienceTimer = patienceTimer;
        patienceSlider.maxValue = patienceTimer;
        
        // Start timer until it reaches 0
        while (_patienceTimer > 0)
        {
            // If player is first - stop the timer
            if (isBeingServed)
            {
                // Destroy the patience slider
                if (patienceSlider != null) 
                {
                    Destroy(patienceSlider.gameObject);
                }
                yield break;
            }
            _patienceTimer -= Time.deltaTime;
            patienceSlider.value = _patienceTimer;
            yield return null;
        }
        
        // If the timer reaches 0
        isUnsatisfied = true;
        // Destroy the patience slider
        gameManager.IncreaseChaosLevel(1f);
        Destroy(patienceSlider.gameObject);

    }


    public void SetDesiredPotion(Potion potion)
    {
        desiredPotion = potion;
    }
    
    public Potion GetDesiredPotion()
    {
        return desiredPotion;
    }
    
    public void SetCustomerPrefab(GameObject prefab)
    {
        customerPrefab = prefab;
    }
    
    public GameObject GetCustomerPrefab()
    {
        return customerPrefab;
    }
    
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
    
    public void SetFirst(bool value)
    {
        isFirst = value;
    }
    
    public bool GetFirst()
    {
        return isFirst;
    }
    
    public bool GetWasServed()
    {
        return wasServed;
    }
    
    public bool GetIsUnsatisfied()
    {
        return isUnsatisfied;
    }
    
    public void SetIsSatisfied(bool value)
    {
        isSatisfied = value;
    }
    
    public bool GetIsSatisfied()
    {
        return isSatisfied;
    }
}
