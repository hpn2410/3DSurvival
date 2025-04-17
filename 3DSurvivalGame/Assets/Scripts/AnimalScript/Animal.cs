using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalData animalData;

    private void Start()
    {
        animalData.currentHealth = animalData.maxHealth;
    }

    private void Update()
    {
        if (animalData.canBeHit)
        {
            GlobalState.Instance.resourcesHeath = animalData.currentHealth;
            GlobalState.Instance.resourcesMaxHeath = animalData.maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        animalData.currentHealth -= damage;
        if (animalData.currentHealth <= 0)
        {
            Debug.LogWarning("This animal is dead");
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animalData.playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animalData.playerInRange = false;
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
        animalData.currentHealth -= EquipSystem.Instance.GetWeaponDamage();
        Debug.LogWarning("being hit, hp left: " + animalData.currentHealth);
        EquipableItem.Instance.canHit = true;

        if (animalData.currentHealth <= 0)
        {
            Debug.LogWarning("This animal is dead");
            Destroy(gameObject);
            SelectionManager.Instance.selectedAnimal = null;
            SelectionManager.Instance.chopHolder.gameObject.SetActive(false);
        }
            
    }
}
