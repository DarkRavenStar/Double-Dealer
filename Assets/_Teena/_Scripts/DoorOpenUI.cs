using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpenUI : MonoBehaviour {


	public GameObject canvas;
	public GameObject textUIPrefab;
	public GameObject doorPanel;

	float timer = 0;

	public bool openDoor = false;
	public bool runOnce = false;
	public bool cannotOpen = false;
	public bool canOpen = false;

	public string closedDoorText;
	public string openedDoorText;

	// Update is called once per frame
	void Update () 
	{
		if (cannotOpen == true) 
		{
			if(runOnce == false)
			{
				canvas = GameObject.FindWithTag ("Canvas");
				doorPanel = Instantiate (textUIPrefab) as GameObject;
				doorPanel.transform.SetParent (canvas.transform, false);
				doorPanel.GetComponentInChildren<Text> ().text = closedDoorText;
				runOnce = true;
				cannotOpen = false;
			}
		}

		if(runOnce == true)
		{
			Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + 1.5f);
			Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
			doorPanel.transform.position = new Vector3(screenPos.x, screenPos.y);
		}

		if(canOpen == true)
		{
			if(runOnce == false)
			{
				canvas = GameObject.FindWithTag ("Canvas");
				doorPanel = Instantiate (textUIPrefab) as GameObject;
				doorPanel.transform.SetParent (canvas.transform, false);
				runOnce = true;
			}
			doorPanel.GetComponentInChildren<Text> ().text = openedDoorText;

			timer++;
			if (timer * Time.deltaTime >= 0  && timer * Time.deltaTime <= 5) 
			{
				doorPanel.GetComponentInChildren<CanvasRenderer> ().SetAlpha (timer * 2 * Time.deltaTime);
			}

			if (timer * Time.deltaTime > 10)
			{
				timer = 0;
				openDoor = false;
				runOnce = false;
				cannotOpen = false;
				canOpen = false;
				doorPanel.SetActive (false);
			}
		}
	}
}
