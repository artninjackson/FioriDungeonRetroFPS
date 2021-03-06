﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public Animator anim;
    public float playerRange = 10f;
    public float meleeRange = 1f;
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
        anim.SetBool("attacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange) 
        {
            anim.SetFloat("playerDistance", 4f);
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < meleeRange)
            {
                rb.velocity = Vector2.zero;
                if (shouldAttack)
                {
                    attackCounter -= Time.deltaTime;
                    if (attackCounter <= 0)
                    {
                        anim.SetBool("attacking", true);
                        Instantiate(attack, attackPoint.position, attackPoint.rotation);
                        attackCounter = attackRate;
                    }
                }
            }

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) >= meleeRange)
            {
                anim.SetBool("attacking", false);
            }
        }

        else
        {
            anim.SetBool("attacking", false);
            anim.SetFloat("playerDistance", 6f);
            rb.velocity = Vector2.zero;
        }
    }

    public void takeDamage()
    {
        health--;
        AudioController.instance.PlayEnemyPain();
        if (health <= 0)
        {
            moveSpeed = 0;
            anim.SetBool("dead", true);
            AudioController.instance.PlayEnemyDeath();
            Destroy(gameObject, 3f);
        }
    }
}
