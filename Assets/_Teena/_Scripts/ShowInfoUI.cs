using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowInfoUI : MonoBehaviour {
	public GameObject canvas;
	public GameObject iconUIPrefab;
	public GameObject iconPanel;

	float timer = 0;
	public bool pickPocketed = false;
	public bool runOnce = false;
	public bool fadedIn = false;
	public bool fadedOut = false;

	// Update is called once per frame
	void Update () 
	{
		if (pickPocketed == true) 
		{
			if(runOnce == false)
			{
				canvas = GameObject.FindWithTag ("Canvas");
				iconPanel = Instantiate (iconUIPrefab) as GameObject;
				iconPanel.transform.SetParent (canvas.transform, false);
				runOnce = true;
			}

			if(runOnce == true)
			{
				Vector3 worldPos = new Vector3(0, 0);
				Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
				iconPanel.transform.position = new Vector3(screenPos.x, screenPos.y);
				if (fadedIn == false) 
				{
					FadeIn ();
					fadedIn = true;
				}

			}

			timer++;

			if (timer * Time.deltaTime > 4)
			{
				timer = 0;
				pickPocketed = false;
				runOnce = false;
				if (fadedOut == false) 
				{
					FadeOut ();
					fadedOut = true;
				}
				
				if (timer * Time.deltaTime > 11) 
				{
					iconPanel.SetActive (false);
					Destroy (iconPanel.gameObject);
				}
				
			}

		}
	}

	void FadeIn()
	{
		iconPanel.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);
		iconPanel.GetComponentInChildren<Image>().CrossFadeAlpha(1f, 1f, false);//second param is the time
	}

	void FadeOut()
	{
		iconPanel.GetComponentInChildren<CanvasRenderer>().SetAlpha(1f);
		iconPanel.GetComponentInChildren<Image>().CrossFadeAlpha(0f, 2f, false);//second param is the time
	}
}


/*
public class PickpocketMotionUI : MonoBehaviour {


	public GameObject canvas;
	public GameObject spriteUIPrefab;
	public GameObject iconPanel;

	public float offSetX;
	public float offSetY;


	Vector2 worldPos;
	Vector2 screenPos;
	Vector3 upperLimit;
	Vector3 lowerLimit;
	Vector3 invPosition;
	float trueSpeed;


	float timer = 0;
	int id;
	[HideInInspector]
	public bool pickPocketed = false;
	public bool runOnce = false;
	public bool flyToInventory = false;
	public bool IsDone = false;
	public bool canPingPong = false;

	// Update is called once per frame
	void Update () 
	{
		
		if(pickPocketed == true)
		{
			timer++;
			if(runOnce == false)
			{
				canvas = GameObject.FindWithTag ("Canvas");
				iconPanel = Instantiate (spriteUIPrefab) as GameObject;
				iconPanel.transform.SetParent (canvas.transform, true);
				runOnce = true;
			}

			if(timer * Time.deltaTime <= 500)
			{
				//Vector3 upperLimit = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + offSetX, transform.position.y + (offSetY + 0.1)));
				//Vector3 lowerLimit = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + offSetX, transform.position.y + (offSetY - 0.1)));
				worldPos = new Vector3(transform.position.x + offSetX, transform.position.y + offSetY);
				//Debug.Log (worldPos);
				screenPos = Camera.main.WorldToScreenPoint(worldPos);
				//Debug.Log (screenPos);
				iconPanel.transform.position = new Vector2(screenPos.x, screenPos.y);
			}


			if(timer * Time.deltaTime > 5)
			{

				//Vector2.SmoothDamp(transform.position, RectTransformUtility.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(uiTarget.position)), ref velocity, smoothTime);				
				//RectTransform invBox = canvas.transform.GetChild (0).GetChild (id).GetComponent<RectTransform> ();
				//trueSpeed = 10 * Time.deltaTime;
				//iconPanel.transform.position = Vector2.MoveTowards (transform.position, RectTransformUtility.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(invBox.position)), trueSpeed);

				worldPos = new Vector3(iconPanel.transform.position.x + 0.1f, iconPanel.transform.position.y + 0.1f);

				screenPos = Camera.main.WorldToScreenPoint(worldPos);

				iconPanel.transform.position = new Vector2(iconPanel.transform.position.x + 0.1f, iconPanel.transform.position.y + 0.1f);

				//Debug.Log(iconPanel.transform.position);
				//Debug.Log(invBox.transform.position);
				//iconPanel.transform.position = new Vector2(screenPos.x + 10, screenPos.y + 10);
				//Debug.Log(iconPanel.transform.position);
			}


			if (iconPanel.transform.position == invPosition) 
			{
				IsDone = true;
				Debug.Log ("Majestic");
				iconPanel.SetActive (false);
			}
		}
	}
}
*/