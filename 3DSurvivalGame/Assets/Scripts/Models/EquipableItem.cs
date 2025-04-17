using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : MonoBehaviour
{
    public static EquipableItem Instance { get; private set; }
    private Animator animatorItem;
    public bool canHit;

    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        animatorItem = GetComponent<Animator>();
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) 
            && !InventorySystem.Instance.isOpen
            && !CraftingSystem.Instance.isOpen && canHit
            && !ConstructionManager.Instance.inConstructionMode)
        {
            GameObject selectedTree = SelectionManager.Instance.selectedTree;
            GameObject selectedAnimal = SelectionManager.Instance.selectedAnimal;
            if (selectedTree != null)
            {
                selectedTree.GetComponent<ChopableObject>().GetHit();
                SoundManager.Instance.PlaySound(SoundManager.Instance.chopSound);
            }
            if(selectedAnimal != null)
            {
                selectedAnimal.GetComponent<Animal>().GetHit();
                SoundManager.Instance.PlaySound(SoundManager.Instance.rabbitHit);
            }
            animatorItem.SetTrigger("Hit");
            SoundManager.Instance.PlaySound(SoundManager.Instance.toolSwingSound);
        }
    }
}
