using System;
using System.Collections;
using UnityEngine;

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
    private bool isFirst = false;
    private bool wasCounterDecremented = false;
    [SerializeField] private bool isBeingServed = false;
    // Customer patience timer
    [SerializeField] private float patienceTimer;
    // Customer satisfaction timer
    [SerializeField] private float satisfactionTimer;
    // Customer Manager reference
    private CustomerManager customerManager;
    
    public Customer(GameObject prefab, Potion potion)
    {
        customerPrefab = prefab;
        desiredPotion = potion;
    }

    private void Start()
    {
        customerManager = GameObject.Find("CustomerManager").GetComponent<CustomerManager>();
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition - new Vector3(0, 0, 15f), 5f * Time.deltaTime);
            if (transform.position == targetPosition - new Vector3(0, 0, 15f))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Move the customer to the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        }



        if (!isBeingServed && transform.position == targetPosition && isFirst)
        {
            isBeingServed = true;
            StartCoroutine(SatisfactionCoroutine());
        }

    }
    
    private IEnumerator SatisfactionCoroutine()
    {
        float _satisfactionTimer = satisfactionTimer;

        // Start timer until it reaches 0
        while (_satisfactionTimer > 0)
        {
            _satisfactionTimer -= Time.deltaTime;
            yield return null;
        }
        
        // If the timer reaches 0
        isUnsatisfied = true;
    }
        
    private IEnumerator PatienceCoroutine()
    {
        float _patienceTimer = patienceTimer;
        // Start timer until it reaches 0
        while (_patienceTimer > 0)
        {
            _patienceTimer -= Time.deltaTime;
            yield return null;
        }
        
        // If the timer reaches 0

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
}
