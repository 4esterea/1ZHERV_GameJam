
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class PlayerInteract : MonoBehaviour
{
    private Input _input;
    [SerializeField] private Vector3 interactOffset;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private bool isHolding;
    [SerializeField] private RecipeBookUI recipeBookUI;
    private Ingredient _heldIngredient;
    private Potion _heldPotion;
    [SerializeField] private Interactable interactable;
    private HashSet<Ingredient> _highlightedIngridients = new HashSet<Ingredient>();
    [SerializeField] private Cauldron cauldron;
    [SerializeField] private Crate crate;
    [SerializeField] private CrateButton button;
    [SerializeField] private Slider cauldronSlider;
    private bool isUsing = false;
    private bool isHoldingP = false;
    private float holdTime = 0f;
    private float requiredHoldTime = 2f;
    private Crate holdingCrate = null;
    private bool areRecipesVisible = false;
    private PotionReceiver potionReceiver = null;
    private void Awake()
    {
        _input = new Input();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (!_input.Player.enabled)
        {
            _input.Player.Enable();
        }
        _input.Player.Use.performed += OnUsePerformed;
        _input.Player.Use.canceled += OnUseCanceled;
        _input.Player.Interact.performed += OnInteractPerformed;
        _input.Player.ToggleRecipeBook.started += OnToggleRecipeBookStarted;
        #if UNITY_EDITOR
            _input.Player.ExitGame.started += context => UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
            _input.Player.ExitGame.started += context => Application.OpenURL(Application.absoluteURL);
        #else
            _input.Player.ExitGame.started += context => Application.Quit();
        #endif
    }

    private void OnDisable()
    {
        if (_input.Player.enabled)
        {
            _input.Player.Disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingP)
        {
            HoldHandling();
        }
        Highlight();
    }

    void OnUsePerformed(InputAction.CallbackContext context)
    {
        isHoldingP = true;
        cauldron = interactable as Cauldron;
        if (cauldron != null)
        {
            cauldronSlider.gameObject.SetActive(true);
            isUsing = true;
        }
    }

    void OnUseCanceled(InputAction.CallbackContext context)
    {
        isHoldingP = false;
        cauldronSlider.gameObject.SetActive(false);
        holdTime = 0f;
    }
    
    void HoldHandling()
    {
        
        cauldron = interactable as Cauldron;
        if (isHoldingP && cauldron != null && isUsing)
        {
            cauldronSlider.gameObject.SetActive(true);
            holdTime += Time.deltaTime;
            cauldronSlider.value = holdTime;
            if (holdTime >= requiredHoldTime)
            {
                cauldron.Cook();
                cauldronSlider.gameObject.SetActive(false);
                isUsing = false;
                holdTime = 0f;
            }
        }
        else
        {
            cauldronSlider.gameObject.SetActive(false);
            holdTime = 0f;
            isUsing = false;
        }
    }
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        cauldron = interactable as Cauldron;
        crate = interactable as Crate;
        button = interactable as CrateButton;
        potionReceiver = interactable as PotionReceiver;
        if (isHolding && interactable == cauldron && cauldron != null && _heldIngredient != null)
        {
            if (!cauldron.IsFull())
            {
                SetHolding(false);

                _heldIngredient.SetHeld(false);

                cauldron.PutIngredient(_heldIngredient);

                _heldIngredient = null;
            }
            else
            {
                Debug.Log("Cauldron is full");
            }

        } 
        else if (isHolding && interactable == potionReceiver && potionReceiver != null && _heldPotion != null)
        {
            potionReceiver.ReceivePotion(_heldPotion);
            
            SetHolding(false);
            _heldPotion = null;
        }
        else if (!isHolding && cauldron != null && cauldron.isReady())
        {
            isHolding = true;
            _heldPotion = cauldron.GetPotion();
            _heldPotion.SetHeld(true);
            _heldPotion.SetPotionReady(false);
            cauldron.DeletePotion();
        }
        else if (isHolding && _heldIngredient != null)
        {
            isHolding = false;
            _heldIngredient.SetWasHeld(true);
            Interact(_heldIngredient);
            _heldIngredient = null;
        }
        else if (isHolding && _heldIngredient == null && holdingCrate == null)
        {
            isHolding = false;
            _heldPotion.SetHeld(false);            
            _heldPotion = null;
        }
        else if (crate != null && !isHolding)
        {
            if (crate.WasTaken())
            {
                Debug.Log("Grabbing ingredient");
                _heldIngredient = crate.GrabIngredient();
                if (_heldIngredient != null)
                {
                    isHolding = true;
                    _heldIngredient.SetWasHeld(true);
                    _heldIngredient.SetInCrate(false);
                    Interact(_heldIngredient);
                }
            }
            else
            {
                Debug.Log("Grabbing crate");
                isHolding = true;
                holdingCrate = crate;
                crate.SetIsHeld(true);
                crate.SetWasTaken(true);
            }
        }
        else if (holdingCrate != null)
        {
            Debug.Log("Releasing crate");
            isHolding = false;
            holdingCrate.SetIsHeld(false);
            holdingCrate = null;
        }
        else if (_highlightedIngridients.Count > 0)
        {
            isHolding = true;
            _heldIngredient = _highlightedIngridients.First();
            _heldIngredient.SetTossed(false);
            Interact(_heldIngredient);
        }
        else if (button != null)
        {
            button.Interact();
        }
    }
    
    void OnToggleRecipeBookStarted(InputAction.CallbackContext context)
    {
        if (areRecipesVisible)
        {
            areRecipesVisible = false;
            recipeBookUI.gameObject.SetActive(false);
        }
        else
        {
            areRecipesVisible = true;
            recipeBookUI.gameObject.SetActive(true);
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void Highlight()
    {
        Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
        Quaternion rotation = transform.rotation;
        Collider[] colliders = Physics.OverlapBox(forwardOffset, Vector3.one / 2, rotation, interactLayer);
        
        interactable = null;
        Ingredient closestIngredient = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            Ingredient ingredient = collider.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                float distance = Vector3.Distance(transform.position, ingredient.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIngredient = ingredient;
                }
            }
            interactable = collider.GetComponent<Interactable>();
        }

        foreach (Ingredient ingredient in _highlightedIngridients)
        {
            ingredient.SetHighlight(false);
        }

        _highlightedIngridients.Clear();

        if (closestIngredient != null)
        {
            closestIngredient.SetHighlight(true);
            _highlightedIngridients.Add(closestIngredient);
        }
    }
    
    
    void Interact(Ingredient ingredient)
    {
        if (isHolding)
        {
            ingredient.SetHeld(true);
        }
        else
        {
            ingredient.SetHeld(false);
        }
    }
    
    public void SetHolding(bool holding)
    {
        isHolding = holding;
    }
    
    public void SetHeldIngredient(Ingredient ingredient)
    {
        _heldIngredient = ingredient;
    }
    private void OnDrawGizmos()
         {
             Vector3 forwardOffset = transform.position + transform.forward + interactOffset;
             Quaternion rotation = transform.rotation;
             Gizmos.matrix = Matrix4x4.TRS(forwardOffset, rotation, Vector3.one);
             Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
         }
         
}
