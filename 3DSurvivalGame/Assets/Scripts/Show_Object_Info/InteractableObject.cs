using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName;
    public string pressToPickUp;
    public bool playerInRange;

    public string GetItemName()
    {
        return itemName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange 
            && SelectionManager.Instance.cursorTarget
            && SelectionManager.Instance.selectedObject == gameObject)
        {
            if(InventorySystem.Instance.CheckSlotsAvailable(1))
            {
                Debug.Log("You picked " + itemName);
                InventorySystem.Instance.AddItemToInventory(itemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

