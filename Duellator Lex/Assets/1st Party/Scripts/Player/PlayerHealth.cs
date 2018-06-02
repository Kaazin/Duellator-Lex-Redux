using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float time, multiplier;  //the base time and mulitplier of hitstun
    public bool isDead;
    public Animator anim;
    public bool isP1;
    public FighterSwitch fSwitch;
    // Use this for initialization

    Slider healthBar;

	void Awake ()
    {
        //assign current health to maxhealth and assign the references
        currentHealth = maxHealth;
        if (isP1)
            healthBar = GameObject.Find("P1HUD").GetComponentInChildren<Slider>();
        else
            healthBar = GameObject.Find("P2HUD").GetComponentInChildren<Slider>();

        healthBar.value = maxHealth;

	}

    void Start()
    {
    anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        healthBar.value = currentHealth;



        //if (currentHealth <= 0 && !isDead)
           // Death();
       

	}

    public void TakeDamage(int amount, float stunTime, float hitstunMultiplier)
    {
        if (!isDead && currentHealth > 0)
        {
            currentHealth -= amount;

                anim.SetTrigger("Hit");
            
            if (currentHealth > 0 && !isDead)
            StartCoroutine(ResetHitStun(time, hitstunMultiplier));

            else if (currentHealth <= 0 && !isDead)
                Death();
        }


    }

    void Death()
    {
        if(!isDead)
        {
            isDead = true;
        
                anim.SetTrigger("Die");
            if(GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            else
                GetComponent<EnemyAI>().enabled = false;

            GetComponent<Rigidbody>().isKinematic = true;


        }
    }

    IEnumerator ResetHitStun(float stunTime, float stunMultiplier)
    {
        yield return new WaitForSeconds(time * stunMultiplier);
            anim.SetTrigger("Reset");
    }
}
