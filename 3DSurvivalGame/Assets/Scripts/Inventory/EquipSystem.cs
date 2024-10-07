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
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeTextColor(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeTextColor(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeTextColor(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            ChangeTextColor(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            ChangeTextColor(5);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            ChangeTextColor(6);
        }
        else if (Input.GetKey(KeyCode.Alpha7))
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
        string selectedItemName = selectedItem.name.Replace("Clone", "");
        GameObject itemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"),
            new Vector3(0.3f, 0.8f, 1f), Quaternion.Euler(0, -110f, 90f));
        itemModel.transform.SetParent(toolHolder.transform, false);
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
}
