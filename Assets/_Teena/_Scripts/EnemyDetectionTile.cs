using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyType;

public class EnemyDetectionTile : MonoBehaviour {

	//For forward enemy Detecion (Attack)
	public int enemyFrontTiles;
	//For backward enemy detection (Pickpocket)
	public int enemyBackTiles;
	//For left enemy detection (Attack)
	public int enemyLeftTiles;
	//For right enemy detection (Attack)
	public int enemyRightTiles;

	public List<GameObject> AttackTiles;// = new List<GameObject>();
	public List<GameObject> PickpocketTiles;// = new List<GameObject>();

	int tempForward;
	int tempBackward;
	int tempLeft;
	int tempRight;

	int curX;
	int curY;

	int attackStateCounter;
	int pickpocketStateCounter;

	bool runOnce = false;

	public ENEMY_TYPE enemyType;
	EnemyAnimManager anim;
	public GameObject player;


	public bool CanSetDetectionPath = true;
	public bool CanRunDetectionPath = false;
    public static bool PlayerGetCatch = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<EnemyAnimManager> ();
		if (enemyType == ENEMY_TYPE.SLEEPING) {
			enemyFrontTiles = 1;
			enemyBackTiles = 1;
			enemyLeftTiles = 1;
			enemyRightTiles = 1;
		}

		if(enemyType == ENEMY_TYPE.ROBOT_GUARD || enemyType == ENEMY_TYPE.HUMAN_GUARD)
		{
			enemyFrontTiles = 4;
			enemyBackTiles = 2;
			enemyLeftTiles = 0;
			enemyRightTiles = 0;
		}
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(CanSetDetectionPath == true)
		{
			ClearDetectionPath ();
			SetDetectionPath ();
			UpdateDetectionPath ();
			CanSetDetectionPath = false;
		}

		if(CanRunDetectionPath == true)
		{
			EnemyAction ();
		}
		*/

		ClearDetectionPath ();
		SetDetectionPath ();
		UpdateDetectionPath ();
		EnemyAction ();
	}

	void ClearDetectionPath()
	{
		if (AttackTiles != null) {
			if (AttackTiles.Count > 0) {
				foreach (GameObject go in AttackTiles) {
					if(go != null) go.GetComponent<SpriteRenderer> ().color = Color.white;
				}
				AttackTiles.Clear ();
			}
		}

		if(PickpocketTiles != null)
		{
			if (PickpocketTiles.Count > 0) {
				foreach (GameObject go in PickpocketTiles) {
					if(go != null) go.GetComponent<SpriteRenderer> ().color = Color.white;
				}
				PickpocketTiles.Clear ();
			}
		}
	}

	void UpdateDetectionPath()
	{
		if (AttackTiles.Count != 0 && AttackTiles != null)
		{
			foreach (GameObject go in AttackTiles)
			{
				if (go != null && go.layer == LayerMask.NameToLayer ("Movable")) 
				{
					go.GetComponent<SpriteRenderer> ().color = Color.red;
				}
			}
		}

		if (PickpocketTiles.Count != 0 && PickpocketTiles != null)
		{
			foreach (GameObject go in PickpocketTiles)
			{
				if(go != null) go.GetComponent<SpriteRenderer>().color = Color.magenta;
			}
		}
	}

	void EnemyAction()
	{
		if(AttackTiles != null && AttackTiles.Count > 0)
		{
			for(int i=0; i<AttackTiles.Count; i++)
			{
				if (player.GetComponent<Movement> ().curX == AttackTiles [i].GetComponent<Node> ().x &&
					player.GetComponent<Movement> ().curY == AttackTiles [i].GetComponent<Node> ().y) 
				{
					Debug.Log("Attack State");
					attackStateCounter++;
				}
			}
		}

		if(PickpocketTiles != null && PickpocketTiles.Count > 0)
		{
			for(int i=0; i<PickpocketTiles.Count; i++)
			{
				if (player.GetComponent<Movement> ().curX == PickpocketTiles [i].GetComponent<Node> ().x &&
					player.GetComponent<Movement> ().curY == PickpocketTiles [i].GetComponent<Node> ().y) 
				{
					Debug.Log ("Pickpocket State");
					pickpocketStateCounter++;
				}
			}
		}

		if(attackStateCounter > 0 && GetComponent<EnemyAIPatrol>().IsAttackState != true) {
			GetComponent<EnemyAIPatrol>().IsAttackState = true;
			//GetComponent<EnemyAIPatrol>().IsAttackState = true;
			player.GetComponent<Movement> ().playerEscaped = true;
			Debug.Log ("Player Escape Attack");
			attackStateCounter = 0;
		}

		if (pickpocketStateCounter > 0) {
			player.GetComponent<Pickpocketing> ().canPlayerPickpocket = true;
			GetComponent<Enemy> ().canBePickpocketed = true;
			Debug.Log ("Pick Active");
			pickpocketStateCounter = 0;
		} else {
			GetComponent<Enemy> ().canBePickpocketed = false;
			Debug.Log ("Pick Deactivated");
		}
	}

	void SetDetectionPath()
	{
		tempForward = enemyFrontTiles;
		tempBackward = enemyBackTiles;
		tempLeft = enemyLeftTiles;
		tempRight = enemyRightTiles;

		curX = GetComponent<EnemyAIPatrol> ().curX;
		curY = GetComponent<EnemyAIPatrol> ().curY;

		if (enemyType == ENEMY_TYPE.ROBOT_GUARD || enemyType == ENEMY_TYPE.HUMAN_GUARD) 
		{
			if (MapPlugin.Instance.FindBlock (curX, curY) != null &&
				MapPlugin.Instance.FindBlock (curX, curY).layer == LayerMask.NameToLayer ("Enemy")) {
				GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY);
				AttackTiles.Add (tempBlock);
			}

			if(anim.forward == true)
			{
				if(tempForward > 0)
				{
					for(int i=1; i < tempForward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY + i) != null &&
						    MapPlugin.Instance.FindBlock (curX, curY + i).layer == LayerMask.NameToLayer ("Movable")) {
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY + i);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempBackward > 0)
				{
					for(int i=1; i < tempBackward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY - i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY - i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY - i);
							PickpocketTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempLeft > 0)
				{
					for(int i=1; i < tempLeft+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX - i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX - i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX - i, curY);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}

				if(tempRight > 0)
				{
					for(int i=1; i < tempRight+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX + i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX + i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX + i, curY);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
			}

			if(anim.backward == true)
			{
				if(tempForward > 0)
				{
					for(int i=1; i < tempForward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY - i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY - i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY - i);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempBackward > 0)
				{
					for(int i=1; i < tempBackward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY + i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY + i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY + i);
							PickpocketTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempLeft > 0)
				{
					for(int i=1; i < tempLeft+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX + i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX + i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX + i, curY);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}

				if(tempRight > 0)
				{
					for(int i=1; i < tempRight+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX - i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX - i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX - i, curY);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
			}

			if(anim.left == true)
			{
				if(tempForward > 0)
				{
					for(int i=1; i < tempForward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX - i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX - i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX - i, curY);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempBackward > 0)
				{
					for(int i=1; i < tempBackward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX + i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX + i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX + i, curY);
							PickpocketTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempLeft > 0)
				{
					for(int i=1; i < tempLeft+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY - i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY - i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY - i);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}

				if(tempRight > 0)
				{
					for(int i=1; i < tempRight+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY + i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY + i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY + i);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
			}

			if(anim.right == true)
			{
				if(tempForward > 0)
				{
					for(int i=1; i < tempForward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX + i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX + i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX + i, curY);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempBackward > 0)
				{
					for(int i=1; i < tempBackward+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX - i, curY) != null &&
							MapPlugin.Instance.FindBlock (curX - i, curY).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX - i, curY);
							PickpocketTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
                
				if(tempLeft > 0)
				{
					for(int i=1; i < tempLeft+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY + i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY + i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY + i);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}

				if(tempRight > 0)
				{
					for(int i=1; i < tempRight+1; i++)
					{
						if (MapPlugin.Instance.FindBlock (curX, curY - i) != null &&
							MapPlugin.Instance.FindBlock (curX, curY - i).layer == LayerMask.NameToLayer ("Movable"))
						{
							GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY - i);
							AttackTiles.Add (tempBlock);
						} else {
							break;
						}
					}
				}
			}
		}

		if(enemyType == ENEMY_TYPE.SLEEPING)
		{
			if(tempForward > 0)
			{
				for(int i=1; i < tempForward+1; i++)
				{
					if (MapPlugin.Instance.FindBlock (curX, curY + i) != null &&
						MapPlugin.Instance.FindBlock (curX, curY + i).layer == LayerMask.NameToLayer ("Movable"))
					{
						GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY + i);
						PickpocketTiles.Add (tempBlock);
					} else {
						break;
					}
				}
			}

			if(tempBackward > 0)
			{
				for(int i=1; i < tempBackward+1; i++)
				{
					if (MapPlugin.Instance.FindBlock (curX, curY - i) != null &&
						MapPlugin.Instance.FindBlock (curX, curY - i).layer == LayerMask.NameToLayer ("Movable"))
					{
						GameObject tempBlock = MapPlugin.Instance.FindBlock (curX, curY - i);
						PickpocketTiles.Add (tempBlock);
					} else {
						break;
					}
				}
			}

			if(tempLeft > 0)
			{
				for(int i=1; i < tempLeft+1; i++)
				{
					if (MapPlugin.Instance.FindBlock (curX - i, curY) != null &&
						MapPlugin.Instance.FindBlock (curX - i, curY).layer == LayerMask.NameToLayer ("Movable"))
					{
						GameObject tempBlock = MapPlugin.Instance.FindBlock (curX - i, curY);
						PickpocketTiles.Add (tempBlock);
					} else {
						break;
					}
				}
			}

			if(tempRight > 0)
			{
				for(int i=1; i < tempRight+1; i++)
				{
					if (MapPlugin.Instance.FindBlock (curX + i, curY) != null &&
						MapPlugin.Instance.FindBlock (curX + i, curY).layer == LayerMask.NameToLayer ("Movable"))
					{
						GameObject tempBlock = MapPlugin.Instance.FindBlock (curX + i, curY);
						PickpocketTiles.Add (tempBlock);
					} else {
						break;
					}
				}
			}
		}
	}
}
