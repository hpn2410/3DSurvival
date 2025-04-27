using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private Slider slider;

    public EnemyData enemyData;

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
        currentHealth = enemyData.currentHeath;
        maxHealth = enemyData.maxHeath;

        // calculate the slider value
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        // Set the value of healthBar
        //healthText.text = currentHealth + "/" + maxHealth;
    }
}
