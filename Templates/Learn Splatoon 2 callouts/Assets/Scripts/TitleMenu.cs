using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleMenu : MonoBehaviour 
{
    Button startButton;

	// Use this for initialization
	void Start () 
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();

            Debug.Log(3);
        }
    }
}
