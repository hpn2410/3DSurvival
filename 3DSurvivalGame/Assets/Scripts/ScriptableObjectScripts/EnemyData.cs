using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScripableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float currentHeath;
    public float maxHeath;
    public float enemyDamage;
    public float attackRange;
}
