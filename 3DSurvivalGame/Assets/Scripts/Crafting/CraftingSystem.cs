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
    public GameObject toolScreenUI, survivalScreenUI, refineScreenUI, buildingScreenUI;

    public List<string> inventoryItemLists = new List<string>();

    // Category Button
    private Button toolsBtn, survivalBtn, refineBtn, buildingBtn;

    //Craft Button
    private Button craftAxeBtn, craftPlankBtn, craftFoundationBtn;

    //Requirement Text;
    private TextMeshProUGUI axeReq1, axeReq2, plankReq1, foundationReq1;

    public bool isOpen;

    // blueprint
    //public BluePrints axeBluePrint = new BluePrints("Axe", "Stone", "Stick", 3, 3, 2, 1);
    //public BluePrints plankBluePrint = new BluePrints("Plank", "Log", 1, 1, 2);

    public BluePrint axeBluePrint;
    public BluePrint plankBluePrint;
    public BluePrint foundationBluePrint;

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

        toolsBtn = craftingScreenUI.transform.Find("Tools_Button").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolCategory(); });

        survivalBtn = craftingScreenUI.transform.Find("Survival_Button").GetComponent<Button>();
        survivalBtn.onClick.AddListener(delegate { OpenSurvivalCategory(); });

        refineBtn = craftingScreenUI.transform.Find("Refine_Button").GetComponent<Button>();
        refineBtn.onClick.AddListener(delegate { OpenRefineCategory(); });

        buildingBtn = craftingScreenUI.transform.Find("Building_Button").GetComponent<Button>();
        buildingBtn.onClick.AddListener(delegate { OnBuildingCategory(); });

        SetUpItem();

    }

    private void SetUpItem()
    {
        // Axe
        axeReq1 = toolScreenUI.transform.Find("Axe").transform.Find("Req1").GetComponent<TextMeshProUGUI>();
        axeReq2 = toolScreenUI.transform.Find("Axe").transform.Find("Req2").GetComponent<TextMeshProUGUI>();

        craftAxeBtn = toolScreenUI.transform.Find("Axe").
            transform.Find("Craft_Axe_Button").GetComponent<Button>();

        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(axeBluePrint); });

        //Plank
        plankReq1 = refineScreenUI.transform.Find("Plank").transform.Find("Req1").GetComponent<TextMeshProUGUI>();

        craftPlankBtn = refineScreenUI.transform.Find("Plank").
            transform.Find("Craft_Plank_Button").GetComponent<Button>();

        craftPlankBtn.onClick.AddListener(delegate { CraftAnyItem(plankBluePrint); });

        //Foundation
        foundationReq1 = buildingScreenUI.transform.Find("Foundation").transform.Find("Req1").GetComponent<TextMeshProUGUI>();

        craftFoundationBtn = buildingScreenUI.transform.Find("Foundation")
            .transform.Find("Craft_Foundation_Button").GetComponent<Button>();

        craftFoundationBtn.onClick.AddListener(delegate { CraftAnyItem(foundationBluePrint); });
    }

    private void CraftAnyItem(BluePrint bluePrintToCraft)
    {

        for (int i = 0; i < bluePrintToCraft.numberProducedItems; i++)
        {
            // Add item to inventory
            InventorySystem.Instance.AddItemToInventory(bluePrintToCraft.itemName);
        }

        if (bluePrintToCraft.totalReq == 1)
        {
            // Remove resources from inventory
            InventorySystem.Instance.RemoveResources(bluePrintToCraft.req1, bluePrintToCraft.amountReq1);
        }
        else if (bluePrintToCraft.totalReq == 2)
        {
            // Remove resources from inventory
            InventorySystem.Instance.RemoveResources(bluePrintToCraft.req1, bluePrintToCraft.amountReq1);
            InventorySystem.Instance.RemoveResources(bluePrintToCraft.req2, bluePrintToCraft.amountReq2);
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
        buildingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    private void OpenSurvivalCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(false);
        buildingScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
    }

    private void OpenRefineCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        buildingScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
    }

    private void OnBuildingCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        buildingScreenUI.SetActive(true);
    }

    public void RefreshNeededItems()
    {
        int stoneCount = 0;
        int stickCount = 0;
        int logCount = 0;
        int plankCount = 0;

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
                case "Log":
                    logCount += 1;
                    break;
                case "Plank":
                    plankCount += 1;
                    break;
            }

            // ------ AXE ------ //
            axeReq1.text = "3 Stone[" + stoneCount + "]";
            axeReq2.text = "3 Stick[" + stickCount + "]";

            if (stoneCount >= 3 && stickCount >= 3 && InventorySystem.Instance.CheckSlotsAvailable(1))
                craftAxeBtn.gameObject.SetActive(true);
            else
                craftAxeBtn.gameObject.SetActive(false);

            // --------- Plank ----------- //
            plankReq1.text = "1 Log[" + logCount + "]";

            if(logCount >= 1 && InventorySystem.Instance.CheckSlotsAvailable(2))
                craftPlankBtn.gameObject.SetActive(true);
            else
                craftPlankBtn.gameObject.SetActive(false);

            // --------- Foundation ---------- //
            foundationReq1.text = "4 Plank[" + plankCount + "]";

            if (plankCount >= 4 && InventorySystem.Instance.CheckSlotsAvailable(4))
                craftFoundationBtn.gameObject.SetActive(true);
            else
                craftFoundationBtn.gameObject.SetActive(false);
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
            survivalScreenUI.SetActive(false);
            refineScreenUI.SetActive(false);
            buildingScreenUI.SetActive(false);

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
