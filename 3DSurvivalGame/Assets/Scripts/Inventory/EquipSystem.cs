using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;
    public List<GameObject> quickSlotsList = new List<GameObject>();

    // -- ItemInfo-- //
    public GameObject numberHolder;
    public int selectedNumber = -1;
    public GameObject selectedItem;

    // -- PlayerHolder --//
    public GameObject toolHolder;

    public GameObject selectedItemModel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        PopulateSlotList();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTextColor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTextColor(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTextColor(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeTextColor(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeTextColor(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeTextColor(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeTextColor(7);
        }
    }

    private void ChangeTextColor(int num)
    {
        if(CheckIfFull(num) == true)
        {
            if (selectedNumber != num)
            {

                selectedNumber = num;

                // Unselect preveously selected item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }

                // set item selected
                selectedItem = GetSelectedItem(num);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquippedModel(selectedItem);

                // Change color
                foreach (Transform child in numberHolder.transform)
                {
                    child.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.white;
                }

                TextMeshProUGUI changeTextNumber = numberHolder.transform
                    .Find("Number" + num).transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

                changeTextNumber.color = Color.red;
            }
            else // press the current numberSlot to deselect that item
            {
                selectedNumber = -1;

                // Unselect preveously selected item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                if (selectedItemModel != null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;
                }

                // Change color
                foreach (Transform child in numberHolder.transform)
                {
                    child.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.white;
                }
            }
        }    
        
    }

    private void SetEquippedModel(GameObject selectedItem)
    {
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"),
            new Vector3(0.9f, 0.6f, 1f), Quaternion.Euler(0, -85f, 90f));
        selectedItemModel.transform.SetParent(toolHolder.transform, false);
    }

    private GameObject GetSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber-1].transform.GetChild(0).gameObject;
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);

        InventorySystem.Instance.RecaculateList();
        QuickSlotSystem.Instance.ReCalculateQuickSlotLists();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsHoldingWeapon()
    {
        if (selectedItem != null)
        {
            if(selectedItem.GetComponent<Weapon>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public int GetWeaponDamage()
    {
        if(selectedItem != null)
        {
            return selectedItem.GetComponent<Weapon>().WeaponData.weaponDamage;
        }
        else
        {
            return 0;
        }
    }
}
