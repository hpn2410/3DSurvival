using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData WeaponData;
    bool isEnemy;
    float distanceWithEnemy;
    Transform player;
    Transform enemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.LogWarning("Hit enemy");
            isEnemy = true;
            distanceWithEnemy = Vector3.Distance(collision.transform.position, transform.position);
            enemy = collision.transform;
        }
    }

    public void DealDamage()
    {
        if (isEnemy && distanceWithEnemy < WeaponData.attackRange)
        {
            enemy.GetComponent<NPCWaypoints>().TakeDamage(WeaponData.weaponDamage);

            isEnemy = false;
            enemy = null;
        }
    }
}
