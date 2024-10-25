using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHeathBar : MonoBehaviour
{

    private Slider slider;
    private float currentHeath, maxHeath;

    public GameObject globalState;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHeath = globalState.GetComponent<GlobalState>().resourcesHeath;
        maxHeath = globalState.GetComponent<GlobalState>().resourcesMaxHeath;

        slider.value = currentHeath / maxHeath;
    }
}
