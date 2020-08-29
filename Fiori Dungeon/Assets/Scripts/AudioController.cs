using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource ammoGet, enemyDeath, enemyHurt, bowShot, healthGet, playerHurt, doorOpen, enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAmmoGet()
    {
        ammoGet.Stop();
        ammoGet.Play();
    }
    public void PlayHealthGet()
    {
        healthGet.Stop();
        healthGet.Play();
    }
    public void PlayBowShot()
    {
        bowShot.Stop();
        bowShot.Play();
    }
    public void PlayEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }
    public void PlayEnemyPain()
    {
        enemyHurt.Stop();
        enemyHurt.pitch = 1.5f;
        enemyHurt.Play();
    }
    public void PlayPlayerPain()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }
    public void PlayDoorOpen()
    {
        doorOpen.Stop();
        doorOpen.Play();
    }
    public void PlayEnemyAttack()
    {
        enemyAttack.Stop();
        enemyAttack.Play();
    }
}
