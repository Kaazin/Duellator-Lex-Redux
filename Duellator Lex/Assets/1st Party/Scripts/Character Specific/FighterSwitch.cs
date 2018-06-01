using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSwitch : MonoBehaviour 

{
    public GameObject[] fighters;
    public float SwitchTime;    //delay between each fighter switch
    public int StartingFighter;
    public bool StartComet;
    ParticleSystem switchFX;    //particle effect for switching characters
	void Awake ()
    {
        //turn off the first fighters and setup the references
        if (StartComet)
            fighters[1].SetActive(false);
        else
            fighters[0].SetActive(false);

        switchFX = GetComponentInChildren<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        //if the tab key is pressed and no other buttons are pressed switch fighters
        if ((!Input.GetButtonDown("Secondary 1") || !Input.GetButtonDown("Secondary 2")) && Input.GetKeyDown(KeyCode.Tab) &&
            (!Input.GetButtonDown("Primary 1") || !Input.GetButtonDown("Primary 2")))
            StartCoroutine(SwitchFighters(SwitchTime));       
    }

    IEnumerator SwitchFighters(float time)
    {
        //if the first fighter is active play the effect and switch to the second after the delay of time
        if (fighters[0].activeSelf)
        {
            switchFX.Stop();
            switchFX.Play();
            yield return new WaitForSeconds(time);
            fighters[1].SetActive(true);
            fighters[0].SetActive(false);
        }

        //if hte second fighter is active play the effect and switch to the first after the delay of time
        else if (fighters[1].activeSelf)
        {
            switchFX.Stop();
            switchFX.Play();
            yield return new WaitForSeconds(time);
            fighters[0].SetActive(true);
            fighters[1].SetActive(false);
        }
    }
}
