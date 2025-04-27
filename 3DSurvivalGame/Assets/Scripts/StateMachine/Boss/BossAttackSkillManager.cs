using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttackSkillManager : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject greenBall;
    public Transform weaponPos;
    public EnemyData enemyData;
    public bool isBossHit;
    public ParticleSystem enemyParticleSystem;
    public Renderer rend;

    float flashDuration = .5f;
    float brightnessMultiplier = 3f;
    Color originalColor;
    NavMeshAgent agent;
    Transform player;
    Animator bossAnimator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        bossAnimator = GetComponent<Animator>();

        originalColor = rend.material.color;
        enemyData.currentHeath = enemyData.maxHeath;
    }

    public void SpawnFireBall()
    {
        Instantiate(fireBall, weaponPos.position, weaponPos.rotation);
    }

    public void SpawnIceBall()
    {
        Instantiate(greenBall, weaponPos.position, weaponPos.rotation);
    }

    public void TakeDamage(int damage)
    {
        enemyData.currentHeath -= damage;

        if(enemyData.currentHeath <= 0 )
        {
            bossAnimator.SetTrigger("Die");
            SoundManager.Instance.PlaySound(SoundManager.Instance.bossDead);
            Debug.LogWarning("Enemy is dead");
            StartCoroutine(Die());
        }
        else
        {
            bossAnimator.SetTrigger("Hit");
            bossAnimator.SetBool("Attack1", false);
            bossAnimator.SetBool("Attack2", false);
            Debug.LogWarning("Enemy is hit");
            enemyParticleSystem.Play();
            SoundManager.Instance.PlaySound(SoundManager.Instance.bossHurt);
            StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        rend.material.color = originalColor * brightnessMultiplier;

        yield return new WaitForSeconds(flashDuration);

        rend.material.color = originalColor;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
