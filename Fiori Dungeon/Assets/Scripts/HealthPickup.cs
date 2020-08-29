using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int HealthAmount = 20;

    // Start is called before the first frame update
    void Start()
    {
        HealthAmount = Random.Range(10, 25);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.instance.currentHealth += HealthAmount;
            PlayerController.instance.UpdateHealthUI();
            AudioController.instance.PlayHealthGet();
            Destroy(gameObject);
        }
    }

}
