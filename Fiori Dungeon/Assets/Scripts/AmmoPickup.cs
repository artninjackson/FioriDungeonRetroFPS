using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        ammoAmount = Random.Range(10, 25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController.instance.currentAmmo += ammoAmount;
            PlayerController.instance.UpdateAmmoUI();
            AudioController.instance.PlayAmmoGet();
            Destroy(gameObject);
        }
    }

}
