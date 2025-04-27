using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    public static Player_State Instance { get; private set; }

    // Player Health
    public float currentHealth, maxHealth;

    // Player Stamina
    public float currentStamina, maxStamina;
    private float moveDistance = 0;
    private Vector3 lastPos;
    public GameObject playerBody;

    // Player Hydration
    public float currentHydration, maxHydration;

    public Transform respawnPosition;
    public GameObject playerGameObject;
    public bool isPlayerDead;
    public GameObject deadCanvas;

    private bool isThristy;
    CharacterController characterController;
    MouseMovement playerMouseMovement;
    PlayerMovement playerMovement;

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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentHydration = maxHydration;

        StartCoroutine(DecreaseHydration());
    }

    private IEnumerator DecreaseHydration()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            currentHydration -= 1f;

            if (currentHydration < 1)
            {
                isThristy = true;
                StartCoroutine(DecreaseHealth());
            }
            else
                isThristy = false;
        }

    }

    private IEnumerator DecreaseHealth()
    {
        while (isThristy)
        {
            yield return new WaitForSeconds(2f);
            currentHealth -= 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseStaminaWhileWalking();
        CheatMinusHealth();
    }

    private void CheatMinusHealth()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 90;
            currentStamina -= 100;
            currentHydration -= 10;
        }
    }

    private void DecreaseStaminaWhileWalking()
    {
        moveDistance += Vector3.Distance(playerBody.transform.position, lastPos);
        lastPos = playerBody.transform.position;

        if (moveDistance >= 20)
        {
            moveDistance = 0;
            currentStamina -= 1f;
        }
    }

    public void setHealth(float health)
    {
        currentHealth = health;
    }
    
    public void setHydration(float hydration)
    {
        currentHydration = hydration;
    }

    public void setStamina(float stamina)
    {
        currentStamina = stamina;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isPlayerDead)
        {
            Debug.LogWarning("You are dead");
            PlayerDeadSound();
            isPlayerDead = true;
            StartCoroutine(RespawnAfterDelay());
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.playerHit);
        }
    }

    private IEnumerator RespawnAfterDelay()
    {
        characterController = playerGameObject.GetComponent<CharacterController>();
        playerMouseMovement = playerGameObject.GetComponent<MouseMovement>();
        playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        characterController.enabled = false;
        playerMouseMovement.enabled = false;
        playerMovement.enabled = false;
        deadCanvas.SetActive(true);
        yield return new WaitForSeconds(10f);

        // Respawn
        transform.position = respawnPosition.position;
        currentHealth = maxHealth;
        currentHydration = maxHydration;
        currentStamina = maxStamina;
        isPlayerDead = false;
        characterController.enabled = true;
        playerMouseMovement.enabled = true;
        playerMovement.enabled = true;
        deadCanvas.SetActive(false);
        Debug.LogWarning("Respawned!");
    }

    private void PlayerDeadSound()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.playerDeath);
    }
}
