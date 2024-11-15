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
            currentHealth -= 10;
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
}
