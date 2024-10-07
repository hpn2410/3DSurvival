using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public TextMeshProUGUI staminaText;

    private Slider slider;

    private float currentStamina, maxStamina;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        StaminaBarUpdate();
    }

    private void StaminaBarUpdate()
    {
        // Update the stamina
        currentStamina = Player_State.Instance.currentStamina;
        maxStamina = Player_State.Instance.maxStamina;

        // Calculate the slider value
        float fillValue = currentStamina / maxStamina;
        slider.value = fillValue;

        // Set text
        staminaText.text = currentStamina + "/" + maxStamina;
    }
}
