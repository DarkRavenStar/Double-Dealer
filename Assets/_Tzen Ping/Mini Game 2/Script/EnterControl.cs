using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterControl : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    private GameObject game;
    public Text disply;
    public Text num;
    public GameObject plyr;
    public static bool isEnter = false;
    public GameObject numberpadlock;
    public GameObject numberpadUnlock;
    public GameObject sound;
	public GameObject propAction;

    public Transform propHolder;
    private void Awake()
    {
        plyr = GameObject.Find("Player");
        propHolder = GameObject.Find("Spawning Tiles").transform.GetChild(0);
    }

    private void Start()
    {
        sound = GameObject.Find("SoundManager");
        numberpadlock.SetActive(true);
        numberpadUnlock.SetActive(false);
    }
    public void OnMouseDown()
    {
        if (clickControl.totalDigits == 4 && clickControl.playerCode == clickControl.correctCode && clickControl.playerCode2 == clickControl.correctCode2 && clickControl.playerCode3 == clickControl.correctCode3 && clickControl.playerCode4 == clickControl.correctCode4)
        {
            SoundManager.Instance.CorrectSource.Play();
			EnterControl.isEnter = true;
            Debug.Log("Win");
            clickControl.totalDigits = 0;
            clickControl.playerCode = "";
            clickControl.playerCode2 = "";
            clickControl.playerCode3 = "";
            clickControl.playerCode4 = "";
            plyr.GetComponent<Movement>().inMiniGame = false;
            numberpadlock.SetActive(false);
            numberpadUnlock.SetActive(true);
            num.text = "";

            for(int i = 0; i < propHolder.childCount; i++)
            {
                if (propHolder.GetChild(i).CompareTag("Hack_Door"))
                {
					if(propHolder.GetChild(i).GetComponent<PropAction>().MinigameType != PropAction.MINIGAMETYPE.ACTIVATE_DOOR)
					{
						propHolder.GetChild(i).GetComponent<PropAction>().MinigameType = PropAction.MINIGAMETYPE.ACTIVATE_DOOR;
					}
                }
            }
			propAction.GetComponent<PropAction> ().MiniGameDone = true;
            game.SetActive(false);
        }
        else
        {
            SoundManager.Instance.WrongSource.Play();
            Debug.Log("Wrong");
        }
    }
    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = new Color32(254, 152, 203, 255);
    }
    void OnMouseExit()
    {

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    private void Update()
    {
        Debug.Log(plyr.GetComponent<Movement>().inMiniGame);
    }
}
