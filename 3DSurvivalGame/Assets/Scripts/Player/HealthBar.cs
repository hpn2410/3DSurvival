using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    public TextMeshProUGUI healthText;

    public GameObject playerState;

    private float currentHealth;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        //healCounter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarUpdate();
    }

    private void HealthBarUpdate()
    {
        currentHealth = playerState.GetComponent<Player_State>().currentHealth;
        maxHealth = playerState.GetComponent<Player_State>().maxHealth;

        // calculate the slider value
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        // Set the value of healthBar
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
