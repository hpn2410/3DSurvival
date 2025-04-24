using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttackSkillManager : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject iceBall;
    public Transform weaponPos;

    NavMeshAgent agent;
    Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void SpawnFireBall()
    {
        Instantiate(fireBall, weaponPos);
    }

    public void SpawnIceBall()
    {
        Instantiate(iceBall, weaponPos);
    }
}
