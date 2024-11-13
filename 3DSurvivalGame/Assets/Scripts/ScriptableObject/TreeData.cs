using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScripableObjects/TreeData")]
public class TreeData : ScriptableObject
{
    [Header("Tree Data")]
    public int m_TreeMaxHealth;
    public int m_TreeCurrentHealth;
    public int m_TreeGetHitDamage;
    public float m_StaminaSpentForChopping;
}
