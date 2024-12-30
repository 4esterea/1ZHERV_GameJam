using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    // Array of customers
    [SerializeField] private Customer[] customerQueue;
    // Array of customer's prefabs
    [SerializeField] GameObject[] customerPrefabs;
    // Customer prefab
    private GameObject customerPrefab;
    // Desired potion
    private Potion desiredPotion;
    // Offset
    [SerializeField] private float offset;
    // Interval between customers
    [SerializeField] private float interval;

    // Array of potions
    [SerializeField] Potion[] potions;
    private bool canSpawn = true;
    
    private int counter = 0;

    private void Update()
    {
        if (counter < 5 && canSpawn)
        {
            StartCoroutine(SpawnCustomerCoroutine());
        }
        ManageQueue();
    }

    // Method to spawn a customer
    private void SpawnCustomer()
    {
        GenerateCustomer();
        // Instantiate the customer prefab 
        GameObject customer = Instantiate(customerPrefab, transform);
        // Set the desired potion for the customer
        customer.GetComponent<Customer>().SetDesiredPotion(desiredPotion);
        customerQueue[counter] = customer.GetComponent<Customer>();

    }

    private void GenerateCustomer()
    {
        // select a random customer prefab
        customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];
        // select a random potion
        desiredPotion = potions[Random.Range(0, potions.Length)];
    }
    
    private void ManageQueue()
    {
        for (int i = 0; i < customerQueue.Length; i++)
        {
            if (customerQueue[i] != null)
            {
                customerQueue[i].SetTargetPosition(new Vector3(-20.7999992f, 0.0599999987f, 7.28999996f + i * offset));
            }
        }

        if (customerQueue[0] != null)
        {
            customerQueue[0].SetFirst(true);
        }
        
        for (int i = 0; i < customerQueue.Length; i++)
        {
            if (customerQueue[i] != null && customerQueue[i].GetIsUnsatisfied())
            {
                for (int j = i; j < customerQueue.Length - 1; j++)
                {
                    customerQueue[j] = customerQueue[j + 1];
                    if (customerQueue[j] != null)
                    {
                        customerQueue[j].SetTargetPosition(new Vector3(-20.7999992f, 0.0599999987f, 7.28999996f + j * offset));
                    }
                }
                customerQueue[customerQueue.Length - 1] = null;
                i--; // Check the current index again after shifting
            }
        }
    }
    
    public void DecrementCounter()
    {
        counter--;
    }
    
    // Coroutine to spawn a customer
    private IEnumerator SpawnCustomerCoroutine()
    {
        canSpawn = false;
        yield return new WaitForSeconds(interval);
        SpawnCustomer();
        counter++;
        canSpawn = true;
    }
    
    public Customer GetFirst()
    {
        return customerQueue[0];
    }

    public Potion[] GetPotions()
    {
        return potions;
    }
}
