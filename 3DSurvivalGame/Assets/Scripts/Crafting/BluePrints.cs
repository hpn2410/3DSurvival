using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BluePrints
{
    public string Name { get; private set; }

    public string Req1 { get; private set; }
    public string Req2 { get; private set; }

    public int AmountReq1 { get; private set; }
    public int AmountReq2 { get; private set; }

    public int TotalReq { get; private set; }

    public int NumberProducedItems { get; private set; }
    public BluePrints(string name, string req1, string req2,
        int amountReq1, int amountReq2, int totalReq, int numberProducedItems) 
    {
        Name = name;
        Req1 = req1;
        Req2 = req2;
        AmountReq1 = amountReq1;
        AmountReq2 = amountReq2;
        TotalReq = totalReq;
        NumberProducedItems = numberProducedItems;
    }
    public BluePrints(string name, string req1,
        int amountReq1, int totalReq, int numberProducedItems)
    {
        Name = name;
        Req1 = req1;
        AmountReq1 = amountReq1;
        TotalReq = totalReq;
        NumberProducedItems = numberProducedItems;
    }
}
