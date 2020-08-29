using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;
    public float attackSpeed = 1;
    public Rigidbody2D rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * attackSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.instance.takeDamage(damageAmount);
            AudioController.instance.PlayEnemyAttack();
            Destroy(gameObject);
        }
    }
}
