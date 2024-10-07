using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrints
{
    public string Name { get; set; }

    public string Req1 {get; set;}
    public string Req2 { get; set; }

    public int AmountReq1 { get; set; }
    public int AmountReq2 { get; set; }

    public int TotalReq { get; set; }

    public BluePrints(string name, string req1, string req2,
        int amountReq1, int amountReq2, int totalReq) 
    {
        Name = name;
        Req1 = req1;
        Req2 = req2;
        AmountReq1 = amountReq1;
        AmountReq2 = amountReq2;
        TotalReq = totalReq;
    }
}
