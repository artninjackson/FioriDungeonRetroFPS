using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public Animator anim;
    public float playerRange = 10f;
    public float meleeRange = 2f;
    public Rigidbody2D rb;
    public float moveSpeed;
    public GameObject attack;
    public bool shouldAttack = true;
    public float attackCounter;
    public float attackRate = 1.5f;
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            anim.SetFloat("playerDistance", 4f);
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= meleeRange)
            {
                rb.velocity = Vector2.zero;
                if (shouldAttack)
                {
                    attackCounter -= Time.deltaTime;
                    if (attackCounter <= 0)
                    {
                        anim.Play("goblin attack", 0, 0f);
                        Instantiate(attack, attackPoint.position, attackPoint.rotation);
                        attackCounter = attackRate;
                    }
                }
            }
        }

        else
        {
            anim.SetFloat("playerDistance", 6f);
            rb.velocity = Vector2.zero;
        }
    }

    public void takeDamage()
    {
        health--;
        //play "ouch" sound
        if(health <= 0)
        {
            //TODO
            //play "death scream" sound
            moveSpeed = 0;
            anim.SetBool("dead", true);
            Destroy(gameObject, 2f);
        }
    }
}
