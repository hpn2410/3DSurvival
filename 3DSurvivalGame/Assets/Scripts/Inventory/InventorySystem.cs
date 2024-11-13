using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // Singleton (Design Pattern)
    public static InventorySystem Instance { get; set; }

    public bool isOpen;

    // Define List
    public List<GameObject> slotLists = new List<GameObject>();
    public List<string> itemLists = new List<string>();

    // Define GameObject
    private GameObject itemToAdd;
    private GameObject slotToEquip;
    public GameObject inventoryScreenUI;
    public GameObject itemInfoUI;

    // Pickup Alert
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupText;
    public Image pickupImage;


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


    void Start()
    {
        isOpen = false;

        ConnectSlotLists();

        Cursor.visible = false;
    }

    private void ConnectSlotLists()
    {
        foreach (Transform child in inventoryScreenUI.transform) 
        {
            if(child.CompareTag("ItemSlot"))
            {
                slotLists.Add(child.gameObject);
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);

            if(!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }

            isOpen = false;
        }
    }

    public void AddItemToInventory(string itemName)
    {
        slotToEquip = FindEmptySlot();

        string nameChange = itemName.Replace("Clone", "");

        itemToAdd = Instantiate(Resources.Load<GameObject>(nameChange),
                slotToEquip.transform.position, slotToEquip.transform.rotation);

        itemToAdd.transform.SetParent(slotToEquip.transform);

        itemLists.Add(itemName);

        Sprite itemSprite = itemToAdd.GetComponent<Image>().sprite;
        PickupAlert(itemName, itemSprite);

        SoundManager.Instance.PlaySound(SoundManager.Instance.pickUpItemSound);
        RecaculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void RemoveResources(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotLists.Count - 1; i >= 0; i--) 
        {
            if(slotLists[i].transform.childCount > 0)
            {
                if (slotLists[i].transform.GetChild(0).name 
                    == nameToRemove + "(Clone)" && counter != 0)
                {
                    DestroyImmediate(slotLists[i].transform.GetChild(0).gameObject);
                    counter -= 1;
                }
            }
        }

        RecaculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    private void PickupAlert(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);
        pickupText.text = itemName;
        pickupImage.sprite = itemSprite;
        StartCoroutine(AlertTime());
    }

    private IEnumerator AlertTime()
    {
        yield return new WaitForSeconds(1f);
        pickupAlert.SetActive(false);
    }    

    public void RecaculateList()
    {
        itemLists.Clear();

        foreach(GameObject slot in slotLists)
        {
            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string clone = "(Clone)";
                string result = name.Replace(clone, "");

                itemLists.Add(result);
            }
        }
    }

    private GameObject FindEmptySlot()
    {
        foreach(GameObject slot in slotLists)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }

    public bool CheckSlotsAvailable(int emptyNeeded)
    {
        int emptySlot = 0;
        foreach (GameObject slot in slotLists)
        {
            if(slot.transform.childCount == 0)
                emptySlot++;
        }

        if(emptySlot >= emptyNeeded)
            return true;
        return false;
    }
}
