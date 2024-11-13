using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScripableObjects/BluePrint")]
public class BluePrint : ScriptableObject
{
    [Header("Blueprint Details")]
    public string itemName;

    [Header("Requirements")]
    public string req1;
    public string req2;

    public int amountReq1;
    public int amountReq2;

    public int totalReq;

    [Header("Produced Items Number Count")]
    public int numberProducedItems;
}
