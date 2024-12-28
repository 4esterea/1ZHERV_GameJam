using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
