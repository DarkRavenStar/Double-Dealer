using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyType;

public class EndDetect : MonoBehaviour {

    private bool canEnd = false;
    public bool winGame = false;
    public GameObject obj;
    public GameObject game;
	public GameObject item;
    float timer = 3f;
    public GameObject plyr;
    public GameObject sound;
    public static bool isCodeAvailable = false;

	// Use this for initialization
	void Start () {
        sound = GameObject.Find("SoundManager");
        plyr = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Pipe2_0.Pipe2_0isConnect == true && Pipe2_1.Pipe2_1isConnect == true && Pipe2_2.Pipe2_2isConnect == true
            && Pipe1_2.Pipe1_2isConnect == true)
        {
            canEnd = true;
        }

        if(winGame == true)
        {
            game.SetActive(false);
			if(item == null)
			{
				obj.SetActive(true);
			}
			else if(item != null)
			{
				if (item.GetComponent<PropAction> () != null) {
					obj.SetActive (true);
				}

				if(item.GetComponent<Enemy> () != null)
				{
					obj.SetActive (false);
                    item.gameObject.SetActive(false);
                    MapPlugin.Instance.FindBlock(item.GetComponent<Node>().x, item.GetComponent<Node>().y).layer = LayerMask.NameToLayer("Movable");
                    for (int i = 0; i < 5; i++) {
                        MapPlugin.Instance.FindBlock(item.GetComponent<Node>().x + i, item.GetComponent<Node>().y).GetComponent<SpriteRenderer>().color = Color.white;
                    }
				}
			}
            plyr.GetComponent<Movement>().inMiniGame = false;
            Time.timeScale = 1f;
            SoundManager.Instance.CorrectSource.Play();
            Invoke("destroy", 4f);
            isCodeAvailable = true;
            /*timer += Time.deltaTime;
            if(timer > Time.deltaTime)
            {
                obj.SetActive(false);
            }
            if(timer < Time.deltaTime)
            {
                obj.SetActive(true);
            }*/
        }
	}

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(canEnd == true)
        {
            if (collision.gameObject.CompareTag("Pipe(2,0)"))
            {
                winGame = true;
            }
        }  
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Pipe(2,0)"))
       {
            winGame = false;
       }
    }

    private void destroy()
    {
        obj.SetActive(false);
    }
}
