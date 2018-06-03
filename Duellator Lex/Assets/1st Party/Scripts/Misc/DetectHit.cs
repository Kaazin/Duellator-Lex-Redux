using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public bool hit = false;
    public GameObject character;
    public bool player = true;
    public string name;
    public GameObject Explosion;
    // Use this for initialization
    AudioSource hitSound;
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.root.tag);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root.tag != this.transform.root.tag && col.transform.tag == "Hurtbox" && !hit)
        {
            if (col.transform.root.name != name)
            {
                PlayerHealth playerHealth = col.GetComponentInParent<PlayerHealth>();
                DuoPlayerHealth duoPlayerHealth = col.GetComponentInParent<DuoPlayerHealth>();

                if (duoPlayerHealth != null)
                    duoPlayerHealth.TakeDamage(10, 1, 1);
                else if (playerHealth != null)
                {
                    Debug.Log(playerHealth.gameObject.name);
                    playerHealth.TakeDamage(10, 1, 1);
                }
                if(hitSound != null)
                hitSound.PlayOneShot(hitSound.clip);
                if (GetComponent<SphereCollider>() != null)
                    GetComponent<SphereCollider>().enabled = false;
                else
                    GetComponent<BoxCollider>().enabled = false;
            }

            //Debug.Log(3);

            if (!player)
            {
                if (transform.root.tag != "VFX")
                {
                    character.GetComponentInChildren<EnemyAI>().DisableLArmBoxes();
                    character.GetComponentInChildren<EnemyAI>().DisableRArmBoxes();
                    character.GetComponentInChildren<EnemyAI>().DisableLLegBoxes();
                    character.GetComponentInChildren<EnemyAI>().DisableRLegBoxes();
                }

   
            }
            else
            {
                if (GetComponentInParent<EnemyAI>() != null)
                    GetComponentInParent<EnemyAI>().attkHit = true;
                if (transform.root.tag != "VFX")
                {
                    character.GetComponentInChildren<PlayerCombat>().DisableLArmBoxes();
                    character.GetComponentInChildren<PlayerCombat>().DisableRArmBoxes();
                    character.GetComponentInChildren<PlayerCombat>().DisableLLegBoxes();
                    character.GetComponentInChildren<PlayerCombat>().DisableRLegBoxes();
                }
                else if (col.transform.root.name != name)
                {
                    GetComponent<SphereCollider>().enabled = false;
                    Destroy(this);
                }

            }

            hit = true;

            hit = false;

        }

        else
        {
            PlayerHealth playerHealth = col.GetComponentInParent<PlayerHealth>();
            DuoPlayerHealth duoPlayerHealth = col.GetComponentInParent<DuoPlayerHealth>();

            if (col.transform.root.name != name)
            {
                if (duoPlayerHealth != null)
                    duoPlayerHealth.TakeDamage(10, 1, 1);
                else if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10, 1, 1);
                }
            }

            Destroy(this);
        }

    }


    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name != name)
        {
            ContactPoint point = col.contacts[0];

            Instantiate(Explosion, point.point, Quaternion.identity);

            if (col.transform.root.name != name)
            {
                if (GetComponent<SphereCollider>() != null)
                    GetComponent<SphereCollider>().enabled = false;
                else
                    GetComponent<BoxCollider>().enabled = false;
                
                PlayerHealth playerHealth = col.transform.GetComponentInParent<PlayerHealth>();
                DuoPlayerHealth duoPlayerHealth = col.transform.GetComponentInParent<DuoPlayerHealth>();

                if (duoPlayerHealth != null)
                    duoPlayerHealth.TakeDamage(10, 1, 1);
                else if (playerHealth != null)
                {
                    Debug.Log(playerHealth.gameObject.name);
                    playerHealth.TakeDamage(10, 1, 1);
                }
            }

        }
        //Debug.Log(1);
    }
}
