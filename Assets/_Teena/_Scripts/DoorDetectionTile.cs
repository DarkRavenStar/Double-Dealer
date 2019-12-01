using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyType;

public class DoorDetectionTile : MonoBehaviour {

	//For forward enemy Detecion (Attack)
	public int enemyFrontTiles;
	//For backward enemy detection (Pickpocket)
	public int enemyBackTiles;
	//For left enemy detection (Attack)
	public int enemyLeftTiles;
	//For right enemy detection (Attack)
	public int enemyRightTiles;

	public List<GameObject> DoorTiles;// = new List<GameObject>();

	int tempForward;
	int tempBackward;
	int tempLeft;
	int tempRight;

	int curX;
	int curY;

	int doorStateCounter;

	bool runOnce = false;
    public bool customPoints = false;

	EnemyAnimManager anim;
	public GameObject player;


	public bool CanSetDetectionPath = true;
	public bool CanRunDetectionPath = false;
    public static bool PlayerGetCatch = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<EnemyAnimManager> ();
        if (customPoints == false) {
            enemyFrontTiles = 1;
            enemyBackTiles = 1;
            enemyLeftTiles = 1;
            enemyRightTiles = 1;
        }
        player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		ClearDetectionPath ();
		SetDetectionPath ();
		UpdateDetectionPath ();
		EnemyAction ();
	}

	public void ClearDetectionPath()
	{
		if(DoorTiles != null)
		{
			if (DoorTiles.Count > 0) {
				foreach (GameObject go in DoorTiles) {
					if(go != null) go.GetComponent<SpriteRenderer> ().color = Color.white;
				}
                DoorTiles.Clear ();
			}
		}
	}

	void UpdateDetectionPath()
	{
		if (DoorTiles.Count != 0 && DoorTiles != null)
		{
			foreach (GameObject go in DoorTiles)
			{
				if(go != null) go.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}

	void EnemyAction()
	{
		if(DoorTiles != null && DoorTiles.Count > 0)
		{
			for(int i=0; i< DoorTiles.Count; i++)
			{
				if (player.GetComponent<Movement> ().curX == DoorTiles[i].GetComponent<Node> ().x &&
					player.GetComponent<Movement> ().curY == DoorTiles[i].GetComponent<Node> ().y) 
				{
                    Debug.Log("door State");
                    doorStateCounter++;
				}
			}
		}

        if (doorStateCounter > 0)
        {
            player.GetComponent<Pickpocketing>().canPlayerPickpocket = true;
            GetComponent<Door>().canOpen = true;
            doorStateCounter = 0;
        }
        else
        {
            GetComponent<Door>().canOpen = false;
        }
    }

	void SetDetectionPath()
	{
		tempForward = enemyFrontTiles;
		tempBackward = enemyBackTiles;
		tempLeft = enemyLeftTiles;
		tempRight = enemyRightTiles;

		curX = GetComponent<Node> ().x;
		curY = GetComponent<Node> ().y;

        if (tempForward > 0)
        {
            for (int i = 1; i < tempForward + 1; i++)
            {
                if (MapPlugin.Instance.FindBlock(curX, curY + i) != null &&
                    MapPlugin.Instance.FindBlock(curX, curY + i).layer == LayerMask.NameToLayer("Movable"))
                {
                    GameObject tempBlock = MapPlugin.Instance.FindBlock(curX, curY + i);
                    DoorTiles.Add(tempBlock);
                }
                else
                {
                    break;
                }
            }
        }

        if (tempBackward > 0)
        {
            for (int i = 1; i < tempBackward + 1; i++)
            {
                if (MapPlugin.Instance.FindBlock(curX, curY - i) != null &&
                    MapPlugin.Instance.FindBlock(curX, curY - i).layer == LayerMask.NameToLayer("Movable"))
                {
                    GameObject tempBlock = MapPlugin.Instance.FindBlock(curX, curY - i);
                    DoorTiles.Add(tempBlock);
                }
                else
                {
                    break;
                }
            }
        }

        if (tempLeft > 0)
        {
            for (int i = 1; i < tempLeft + 1; i++)
            {
                if (MapPlugin.Instance.FindBlock(curX - i, curY) != null &&
                    MapPlugin.Instance.FindBlock(curX - i, curY).layer == LayerMask.NameToLayer("Movable"))
                {
                    GameObject tempBlock = MapPlugin.Instance.FindBlock(curX - i, curY);
                    DoorTiles.Add(tempBlock);
                }
                else
                {
                    break;
                }
            }
        }

        if (tempRight > 0)
        {
            for (int i = 1; i < tempRight + 1; i++)
            {
                if (MapPlugin.Instance.FindBlock(curX + i, curY) != null &&
                    MapPlugin.Instance.FindBlock(curX + i, curY).layer == LayerMask.NameToLayer("Movable"))
                {
                    GameObject tempBlock = MapPlugin.Instance.FindBlock(curX + i, curY);
                    DoorTiles.Add(tempBlock);
                }
                else
                {
                    break;
                }
            }
        }
	}
}
