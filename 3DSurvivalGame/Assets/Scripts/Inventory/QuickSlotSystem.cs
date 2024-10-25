using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotSystem : MonoBehaviour
{
    public List<GameObject> quickSlotLists = new List<GameObject>();
    public List<string> itemLists = new List<string>();

    public static QuickSlotSystem Instance { get; set; }
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

    public void ReCalculateQuickSlotLists()
    {
        itemLists.Clear();
        foreach (GameObject slot in quickSlotLists)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string clone = "(Clone)";
                string result = name.Replace(clone, "");

                itemLists.Add(result);
            }
        }
    }
}
