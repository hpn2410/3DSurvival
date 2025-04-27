using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Transform player;
    Animator bossAnimator;
    Vector3 direction;
    float timer;

    public float speed = 5f;
    public WeaponData weaponData;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossAnimator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        direction = (player.position - transform.position).normalized;

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Destroy gameobject if miss
        timer += Time.deltaTime;
        if(timer >= 10)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player_State.Instance.TakeDamage(weaponData.weaponDamage);
            
            if(Player_State.Instance.isPlayerDead)
            {
                bossAnimator.SetTrigger("IsPlayerDie");
            }

            Destroy(gameObject);
        }
    }
}
