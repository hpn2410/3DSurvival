using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalData animalData;
    private Animator animator;

    public ParticleSystem hitParticleSystem;
    public Renderer rend;

    float flashDuration = .3f;
    float brightnessMultiplier = 2f;
    Color originalColor;

    private void Start()
    {
        animalData.currentHealth = animalData.maxHealth;
        animator = GetComponent<Animator>();
        originalColor = rend.material.color;

        //hitParticleSystem.Stop();
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
            GetComponent<AiMovement>().enabled = false;

            Debug.LogWarning("This animal is dead");

            StartCoroutine(AnimalDead());
        }
    }

    private IEnumerator AnimalDead()
    {
        animator.SetTrigger("isDead");

        yield return new WaitForSeconds(1.3f);

        Destroy(gameObject);
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

    IEnumerator FlashRoutine()
    {
        rend.material.color = originalColor * brightnessMultiplier;

        yield return new WaitForSeconds(flashDuration);

        rend.material.color = originalColor;
    }

    private IEnumerator HitDelay()
    {
        EquipableItem.Instance.canHit = false;
        hitParticleSystem.Play();
        yield return new WaitForSeconds(0.65f);
        animalData.currentHealth -= EquipSystem.Instance.GetWeaponDamage();
        Debug.LogWarning("being hit, hp left: " + animalData.currentHealth);
        EquipableItem.Instance.canHit = true;

        if (animalData.currentHealth <= 0)
        {
            Debug.LogWarning("This animal is dead");
            
            SelectionManager.Instance.selectedAnimal = null;
            SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

            StartCoroutine(AnimalDead());
        }
            
    }
}
