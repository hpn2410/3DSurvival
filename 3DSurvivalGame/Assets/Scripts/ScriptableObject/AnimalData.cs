using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScripableObjects/AnimalData")]
public class AnimalData : ScriptableObject
{
    public string animalName;
    public bool playerInRange;
    public bool canBeHit;
    public int currentHealth;
    public int maxHealth;
}
