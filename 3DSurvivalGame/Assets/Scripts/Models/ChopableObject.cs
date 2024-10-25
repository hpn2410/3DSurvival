using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChopableObject : MonoBehaviour
{
    // player interaction
    public bool playerInRange;
    public bool canBeDropped;

    // tree properties
    public float treeMaxHealth;
    public float treeHeath;
    public Animator treeAnim;

    public float staminaSpentForChopping = 10;

    private void Start()
    {
        treeHeath = treeMaxHealth;
        treeAnim = transform.parent.transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if(canBeDropped)
        {
            GlobalState.Instance.resourcesHeath = treeHeath;
            GlobalState.Instance.resourcesMaxHeath = treeMaxHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

    public void GetHit()
    {
        StartCoroutine(HitDelay());
    }

    private IEnumerator HitDelay()
    {
        EquipableItem.Instance.canHit = false;
        yield return new WaitForSeconds(0.65f);
        treeAnim.SetTrigger("Shake");
        treeHeath -= 2;
        Player_State.Instance.currentStamina -= staminaSpentForChopping;
        EquipableItem.Instance.canHit = true;

        if (treeHeath <= 0)
            DestroyTree();
    }

    private void DestroyTree()
    {
        Vector3 treePos = transform.position;

        Destroy(transform.parent.transform.parent.gameObject);
        canBeDropped = false;
        SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"), treePos, Quaternion.Euler(0, 0, 0));
    }
}
