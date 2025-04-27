using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class NPCWaypoints : MonoBehaviour
{
    public GameObject npc_Waypoints;
    public EnemyData enemyData;
    public ParticleSystem enemyParticleSystem;
    public Renderer rend;

    float flashDuration = .5f;
    float brightnessMultiplier = 3f;
    Color originalColor;
    Transform player;
    NavMeshAgent agent;
    Animator enemyAnimator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();

        originalColor = rend.material.color;
        enemyData.currentHeath = enemyData.maxHeath;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6f); // Attacking Distance

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); // Start Chasing Distance

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 25f); // Stop Chasing Distance
    }

    public void TryAttack()
    {
        float distance = Vector3.Distance(player.position, agent.transform.position);

        if (distance <= enemyData.attackRange)
        {
            Player_State.Instance.TakeDamage(enemyData.enemyDamage);

            if(Player_State.Instance.isPlayerDead)
            {
                enemyAnimator.SetTrigger("IsPlayerDie");
            }
        }
        else
        {
            Debug.LogWarning("Dogde");
        }
    }

    IEnumerator FlashRoutine()
    {
        rend.material.color = originalColor * brightnessMultiplier;

        yield return new WaitForSeconds(flashDuration);

        rend.material.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        enemyData.currentHeath -= damage;

        if (enemyData.currentHeath <= 0)
        {
            enemyAnimator.SetTrigger("Die");
            SoundManager.Instance.PlaySound(SoundManager.Instance.bearDead);
            Debug.LogWarning("Enemy is dead");
            StartCoroutine(Die());
        }
        else
        {
            enemyAnimator.SetTrigger("Hit");
            enemyParticleSystem.Play();
            SoundManager.Instance.PlaySound(SoundManager.Instance.bearHit);
            StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
