using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public static GlobalState Instance { get; set; }

    public float resourcesHeath;
    public float resourcesMaxHeath;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
