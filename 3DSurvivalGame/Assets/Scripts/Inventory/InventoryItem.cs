using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;

    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;
    public float staminaEffect;
    public float hydrationEffect;

    // --- Equipping --- //
    public bool isEquipable;
    private GameObject itemPendingEquipping;
    public GameObject itemPendingToBeUsed;
    public bool isInsideQuickSlot;
    public bool isSelected;
    public bool isUseAble;


    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.itemInfoUI;
        itemInfoUI_itemName = itemInfoUI.transform.Find("Item_Name")
            .GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("Item_Des")
            .GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("Item_Functionality")
            .GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(isSelected)
        {
            gameObject.GetComponent<DragAndDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragAndDrop>().enabled = true;
        }
    }

    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, staminaEffect, hydrationEffect);
            }

            if (isEquipable && !isInsideQuickSlot && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }

            if(isUseAble)
            {
                itemPendingToBeUsed = gameObject;
                UseItem();
            }
        }
    }

    private void UseItem()
    {
        itemInfoUI.SetActive(false);

        InventorySystem.Instance.isOpen = false;
        InventorySystem.Instance.inventoryScreenUI.SetActive(false);

        CraftingSystem.Instance.isOpen = false;
        CraftingSystem.Instance.craftingScreenUI.SetActive(false);
        CraftingSystem.Instance.toolScreenUI.SetActive(false);
        CraftingSystem.Instance.survivalScreenUI.SetActive(false);
        CraftingSystem.Instance.refineScreenUI.SetActive(false);
        CraftingSystem.Instance.buildingScreenUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SelectionManager.Instance.EnableSelection();
        SelectionManager.Instance.enabled = true;

        switch(gameObject.name)
        {
            case "Foundation(Clone)":

                break;
        }
    }

    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.RecaculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }

            if(isUseAble && itemPendingToBeUsed == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.RecaculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }
    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect)
    {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);

        staminaEffectCalculation(caloriesEffect);

        hydrationEffectCalculation(hydrationEffect);

    }


    private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //

        float healthBeforeConsumption = Player_State.Instance.currentHealth;
        float maxHealth = Player_State.Instance.maxHealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                Player_State.Instance.setHealth(maxHealth);
            }
            else
            {
                Player_State.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }


    private static void staminaEffectCalculation(float staminaEffect)
    {
        // --- Calories --- //

        float staminaBeforeConsumption = Player_State.Instance.currentStamina;
        float maxStamina = Player_State.Instance.maxStamina;

        if (staminaEffect != 0)
        {
            if ((staminaBeforeConsumption + staminaEffect) > maxStamina)
            {
                Player_State.Instance.setStamina(maxStamina);
            }
            else
            {
                Player_State.Instance.setStamina(staminaBeforeConsumption + staminaEffect);
            }
        }
    }


    private static void hydrationEffectCalculation(float hydrationEffect)
    {
        // --- Hydration --- //

        float hydrationBeforeConsumption = Player_State.Instance.currentHydration;
        float maxHydration = Player_State.Instance.maxHydration;

        if (hydrationEffect != 0)
        {
            if ((hydrationBeforeConsumption + hydrationEffect) > maxHydration)
            {
                Player_State.Instance.setHydration(maxHydration);
            }
            else
            {
                Player_State.Instance.setHydration(hydrationBeforeConsumption + hydrationEffect);
            }
        }
    }
}
