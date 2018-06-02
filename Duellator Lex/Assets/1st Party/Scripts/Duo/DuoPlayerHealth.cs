﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoPlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float time, multiplier;  //the base time and mulitplier of hitstun
    public bool isDead;
    public Animator anim;
    public GameObject[] fighters;

    public FighterSwitch fSwitch;
    // Use this for initialization
    void Awake()
    {
        //assign current health to maxhealth and assign the references
        currentHealth = maxHealth;
    }

    void Start()
    {
            fighters[0] = fSwitch.fighters[0];
            fighters[1] = fSwitch.fighters[1];
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            TakeDamage(10, time, multiplier);


        //if (currentHealth <= 0 && !isDead)
        // Death();


    }

    public void TakeDamage(int amount, float stunTime, float hitstunMultiplier)
    {
        if (!isDead && currentHealth > 0)
        {
            currentHealth -= amount;

                fighters[fSwitch.currentFighterIndex].GetComponentInChildren<Animator>().SetTrigger("Hit");
      

            if (currentHealth > 0 && !isDead)
                StartCoroutine(ResetHitStun(time, hitstunMultiplier));

            else if (currentHealth <= 0 && !isDead)
                Death();
        }


    }

    void Death()
    {
        if (!isDead)
        {
            isDead = true;
            fighters[fSwitch.currentFighterIndex].GetComponentInChildren<Animator>().SetTrigger("Die");
            GetComponent<FighterSwitch>().enabled = false;
            GetComponent<DuoPlayerMovement>().enabled = false;
        }
    }

    IEnumerator ResetHitStun(float stunTime, float stunMultiplier)
    {
        yield return new WaitForSeconds(time * stunMultiplier);
            fighters[fSwitch.currentFighterIndex].GetComponentInChildren<Animator>().SetTrigger("Reset");
    }
}
