using UnityEngine;

public class PotionReceiver : Interactable
{
    private Potion _potion;
    private Customer _customer;
    
    private bool isPotionReceived = false;
    
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
    
    public void ReceivePotion(Potion potion)
    {
        isPotionReceived = true;
        potion.gameObject.SetActive(false);
        _potion = potion;
        Debug.Log("Potion received");
    }
    
    public bool CompareCustomerPotion(Potion potion)
    {

        if (_potion.name.Replace("(Clone)", "").Trim() == potion.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void SetCustomer(Customer customer)
    {
        _customer = customer;
    }
    
    public bool GetIsPotionReceived()
    {
        return isPotionReceived;
    }
    
    public void SetIsPotionReceived(bool value)
    {
        isPotionReceived = value;
    }

    public void DestroyPotion()
    {
        Destroy(_potion.gameObject);
        SetIsPotionReceived(false);
    }
}
