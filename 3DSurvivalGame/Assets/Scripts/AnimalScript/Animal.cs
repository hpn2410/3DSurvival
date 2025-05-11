using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalData animalData;
    private Animator animator;

    public ParticleSystem hitParticleSystem;
    public Renderer rend;
    public bool playerInRange;
    public bool canBeHit;
    public GameObject meatSpawn;

    float flashDuration = .3f;
    float brightnessMultiplier = 2f;
    Color originalColor;

    float currentHeath = 0;
    float maxHeath = 30;

    private void Start()
    {
        animalData.currentHealth = animalData.maxHealth;
        animator = GetComponent<Animator>();
        originalColor = rend.material.color;


        currentHeath = maxHeath;
        //hitParticleSystem.Stop();
    }

    private void Update()
    {
        if (canBeHit)
        {
            GlobalState.Instance.resourcesHeath = currentHeath;
            GlobalState.Instance.resourcesMaxHeath = maxHeath;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHeath -= damage;
        if (currentHeath <= 0)
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

        Instantiate(meatSpawn, animator.transform.position, animator.transform.rotation);
        Destroy(gameObject);
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
        currentHeath -= EquipSystem.Instance.GetWeaponDamage();
        Debug.LogWarning("being hit, hp left: " + currentHeath);
        EquipableItem.Instance.canHit = true;

        if (currentHeath <= 0)
        {
            Debug.LogWarning("This animal is dead");
            
            SelectionManager.Instance.selectedAnimal = null;
            SelectionManager.Instance.chopHolder.gameObject.SetActive(false);

            StartCoroutine(AnimalDead());
        }
            
    }
}
