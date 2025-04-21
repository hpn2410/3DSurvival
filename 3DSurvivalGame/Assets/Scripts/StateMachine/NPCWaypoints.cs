using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class NPCWaypoints : MonoBehaviour
{
    public GameObject npc_Waypoints;
    public EnemyData enemyData;

    Transform player;
    NavMeshAgent agent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        enemyData.currentHeath = enemyData.maxHeath;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6f); // Attacking Distance

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); // Start Chasing Distance

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 21f); // Stop Chasing Distance
    }

    public void TryAttack()
    {
        float distance = Vector3.Distance(player.position, agent.transform.position);

        if (distance <= enemyData.attackRange)
        {
            Player_State.Instance.TakeDamage(enemyData.enemyDamage);
        }
        else
        {
            Debug.LogWarning("Dogde");
        }
    }

    public void TakeDamage(int damage)
    {
        enemyData.currentHeath -= damage;

        if(enemyData.currentHeath <= 0)
        {
            Debug.LogWarning("Enemy is dead");
        }
    }
}
