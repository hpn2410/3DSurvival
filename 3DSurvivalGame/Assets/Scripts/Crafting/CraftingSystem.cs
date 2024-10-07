using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; set; }

    public GameObject craftingScreenUI;
    public GameObject toolScreenUI;

    public List<string> inventoryItemLists = new List<string>();

    // Category Button
    private Button toolsBtn;

    //Craft Button
    private Button craftAxeBtn;

    //Requirement Text;
    private TextMeshProUGUI axeReq1, axeReq2;

    public bool isOpen;

    // blueprint
    public BluePrints axeBluePrint = new BluePrints("Axe", "Stone", "Stick", 3, 3, 2);

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        //craftingScreenUI = GameObject.FindWithTag("CraftingScreenUI");
        //toolScreenUI = GameObject.FindWithTag("ToolScreenUI");

        toolsBtn = craftingScreenUI.transform.Find("Tools_Button").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolCategory(); });

        // Axe
        axeReq1 = toolScreenUI.transform.Find("Axe").transform.Find("Req1").GetComponent<TextMeshProUGUI>();
        axeReq2 = toolScreenUI.transform.Find("Axe").transform.Find("Req2").GetComponent<TextMeshProUGUI>();

        craftAxeBtn = toolScreenUI.transform.Find("Axe").
            transform.Find("Craft_Axe_Button").GetComponent<Button>();

        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(axeBluePrint); });
    }

    private void CraftAnyItem(BluePrints bluePrintToCraft)
    {
        // Add item to inventory
        InventorySystem.Instance.AddItemToInventory(bluePrintToCraft.Name);

        if (bluePrintToCraft.TotalReq == 1)
        {
            // Remove resources from inventory
            InventorySystem.Instance.RemoveResources(bluePrintToCraft.Req1, bluePrintToCraft.AmountReq1);
        }
        else if (bluePrintToCraft.TotalReq == 2)
        {
            // Remove resources from inventory
            InventorySystem.Instance.RemoveResources(bluePrintToCraft.Req1, bluePrintToCraft.AmountReq1);
            InventorySystem.Instance.RemoveResources(bluePrintToCraft.Req2, bluePrintToCraft.AmountReq2);
        }

        // Refersh list
        StartCoroutine(calculate());
    }

    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(0.5f);

        InventorySystem.Instance.RecaculateList();

        RefreshNeededItems();
    }

    private void OpenToolCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    public void RefreshNeededItems()
    {
        int stoneCount = 0;
        int stickCount = 0;

        inventoryItemLists = InventorySystem.Instance.itemLists;

        foreach(string itemName in inventoryItemLists)
        {
            switch(itemName)
            {
                case "Stone":
                    stoneCount += 1;
                    break;
                case "Stick":
                    stickCount += 1;
                    break;
            }

            // ------ AXE ------ //
            axeReq1.text = "3 Stone[" + stoneCount + "]";
            axeReq2.text = "3 Stick[" + stickCount + "]";

            if (stoneCount >= 3 && stickCount >= 3)
            {
                craftAxeBtn.gameObject.SetActive(true);
            }
            else
            {
                craftAxeBtn.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }

            isOpen = false;
        }
    }
}
