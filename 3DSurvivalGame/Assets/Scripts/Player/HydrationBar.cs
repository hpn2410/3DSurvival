using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;

    public TextMeshProUGUI hydrationText;

    private float currentHydration, maxHydration;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HydrationBarUpdate();
    }

    private void HydrationBarUpdate()
    {
        // Update the value bar
        currentHydration = Player_State.Instance.currentHydration;
        maxHydration = Player_State.Instance.maxHydration;

        // Calculate the slider value
        float fillValue = currentHydration / maxHydration;
        slider.value = fillValue;

        // Set text
        hydrationText.text = currentHydration + "%";
    }
}
