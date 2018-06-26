using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public InputField[] currentCallouts;        //the list of callouts

    string guessString;     //the guess of the callout
    string str;
    public int questionNum;    //what callout are we on
    int startingQuestion = 0;
    int calloutsLength; //how long is the list of callouts?
    public string calloutName;  //which map should the callouts derive from

    Image currentField;     //image attached to the current field 

	void Awake()
	{
        //set up refereces and assign question num's value
        currentCallouts = GameObject.Find(calloutName).GetComponentsInChildren<InputField>();

        questionNum = 0;
        Debug.Log(currentCallouts[0]);

        calloutsLength = currentCallouts.Length;

        str = currentCallouts[questionNum].transform.name;

        currentField = currentCallouts[questionNum].GetComponent<Image>();

        currentField.color = Color.green;
      }


    public void GetInput(string guess)
    {
        CompareGuess(guess);
    }

    void CompareGuess(string guess)
    {
        //get the current field
        currentField = currentCallouts[questionNum].GetComponent<Image>();

        //if the player guessed right
        if (guess == str)
        {


            //if we are on the last question
            if ((questionNum == calloutsLength - 1))
            {
                //the player has won
                Debug.Log("You win");
                currentCallouts[questionNum].gameObject.SetActive(false);

            }
            //otherwise
            else
            {
                //move to the next quesiton and make the last input field inactive
                currentCallouts[questionNum].gameObject.SetActive(false);
                questionNum++;
                //turn on the current text box
                currentCallouts[questionNum].gameObject.SetActive(true);
                //reassign the value of str to the answer for the current textbox
                str = currentCallouts[questionNum].transform.name;

                //assign the value to current field and make it green to indicate that it is the current field
                currentField = currentCallouts[questionNum].GetComponent<Image>();
                currentField.color = Color.green;
            }
        }
        //otherwise the question was incorrect turn the field rec
        else
            currentField.color = Color.red;

    }

	private void Update()
	{
        for (int i = 0; i < calloutsLength; i++)
        {
            //Debug.Log(i);

            if (currentCallouts[i].name != str)
            {
                currentCallouts[i].DeactivateInputField();
                Debug.Log(currentCallouts[i].name);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }
    void Quit()
    {
        Application.Quit();
        Debug.Log(3);
    }

}
   

