using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl1 : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
        if (EnterControl.isEnter == true && clickControl.totalDigits < 4 )
        {
            GetComponent<TextMesh>().text = "Password are 4 digits!!!";
            EnterControl.isEnter = false;
        }
        else if (EnterControl.isEnter == true && clickControl.totalDigits == 4 && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "All Number Incorrect!!";
        }
        else if(EnterControl.isEnter ==true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Second Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Third Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Fourth Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First, Second Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First, Third Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First, Fourth Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First, Second, Third Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First, Second, Fourth Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "First, Third, Fourth Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 != clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Second, Third Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 != clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Second, Fourth Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Second, Third, Fourth Number Correct!!";
        }
        else if (EnterControl.isEnter == true && clickControl.playerCode != clickControl.correctCode && clickControl.playerCode2 != clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            GetComponent<TextMesh>().text = "Third, Fourth Number Correct!!";
        }
    }
}
