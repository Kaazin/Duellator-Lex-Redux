using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DuoPlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float time, multiplier;  //the base time and mulitplier of hitstun
    public bool isDead;
    public Animator anim;
    public GameObject[] fighters;
    public bool isP1;
    public FighterSwitch fSwitch;

    Slider healthBar;
    // Use this for initialization
    void Awake()
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
            fighters[0] = fSwitch.fighters[0];
            fighters[1] = fSwitch.fighters[1];
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;

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

            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator ResetHitStun(float stunTime, float stunMultiplier)
    {
        yield return new WaitForSeconds(time * stunMultiplier);
            fighters[fSwitch.currentFighterIndex].GetComponentInChildren<Animator>().SetTrigger("Reset");
    }
}
