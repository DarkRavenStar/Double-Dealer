using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PressurePoint {
	public int x, y;
	// [HideInInspector]
	public GameObject plate;

	public bool IsPressureOn = false;
	public bool plateDown = false;
	public bool ToBeReseted = false;

	public float originalPosX;
	public float originalPosY;

	public PressurePoint(int xPoint, int yPoint)
	{
		x = xPoint;
		y = yPoint;
	}
};

public class PressurePlateMiniGame : MonoBehaviour {

	[SerializeField] private List <PressurePoint> originalPressurePoint = new List<PressurePoint> ();
	[SerializeField] private static List <EnemyAIPatrol> enemies;

    public Sprite PressureTile;

    public static List<GameObject> doors;
    public List <PressurePoint> enemyPressurePoint;
	public List <PressurePoint> playerPressurePoint;

    public static Transform pHolder;
    public GameObject sound;
    public GameObject player;

	public bool AllSetUp = false;
	public bool IsComplete = false;
	public bool IsDone = false;
	public bool InOrder =  false;
    public bool IsCorrect = false;
	public int pressureObject = 0;
	public int InOrderCount = 0;

	public float timer = 0;


	// Use this for initialization
	void Start () {
        sound = GameObject.Find("SoundManager");
		player = GameObject.FindWithTag ("Player");
        pHolder = GameObject.Find("Spawning Tiles").transform.GetChild(0);  
		for(int i = 0; i < originalPressurePoint.Count; i++)
		{
			GameObject tempPlate = MapPlugin.Instance.FindBlock (originalPressurePoint[i].x, originalPressurePoint[i].y);
			if (tempPlate != null) {
				if (originalPressurePoint [i].plate == null)
				{
					originalPressurePoint [i].plate = tempPlate;
					originalPressurePoint [i].originalPosX = tempPlate.GetComponent<Transform> ().transform.position.x;
					originalPressurePoint [i].originalPosY = tempPlate.GetComponent<Transform> ().transform.position.y;
				}
			}
			else
			{
				Debug.Log ("Plates coordinate are incomplete");
				break;
			}

			if (i == originalPressurePoint.Count - 1) 
			{
				AllSetUp = true;
				Debug.Log ("AllSetUp = true");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(AllSetUp == true)
		{
			EnemyCoordinates ();
			CheckPressurePoints();
			UpdatePlates ();
			CrossCheckPressurePoints ();
		}

	}

	void CheckPressurePoints()
	{

		if (originalPressurePoint != null && originalPressurePoint.Count > 0) {

			if (IsComplete == false && player.GetComponent<Movement> ().playerAtRest == true) {
				for (int i = 0; i < originalPressurePoint.Count; i++) {
					if (player.GetComponent<Movement> ().curX == originalPressurePoint [i].plate.GetComponent<Node> ().x &&
					    player.GetComponent<Movement> ().curY == originalPressurePoint [i].plate.GetComponent<Node> ().y) {
						if (playerPressurePoint.Count < originalPressurePoint.Count + 1 && !playerPressurePoint.Contains (originalPressurePoint [i])) {

							if (originalPressurePoint [i].IsPressureOn == false) {
                                sound.GetComponent<SoundManager>().PressureSource.Play();
								originalPressurePoint [i].IsPressureOn = true;
							}

							playerPressurePoint.Add (originalPressurePoint [i]);   
						}
					}
				}
			}
				
			if (enemies != null && enemies.Count > 0) {
				for (int i = 0; i < enemies.Count; i++) {
					if (enemies[i].enemyReachedIt == true) {
						for (int j = 0; j < originalPressurePoint.Count; j++) {
							if (enemies [i].curX == originalPressurePoint [j].plate.GetComponent<Node> ().x &&
								enemies [i].curY == originalPressurePoint [j].plate.GetComponent<Node> ().y) {
								if (!enemyPressurePoint.Contains (originalPressurePoint [j])) {
									if (originalPressurePoint [j].IsPressureOn == false) {
										originalPressurePoint [j].IsPressureOn = true;
										Debug.Log ("Pressure Point: " + originalPressurePoint [j] + " status: " + originalPressurePoint [j].IsPressureOn);
									}
									enemyPressurePoint.Add (originalPressurePoint [j]);
								}
							}


						}
					}
				}
			}



			if (enemies != null && enemies.Count > 0) {
				for (int i = 0; i < enemies.Count; i++) {
					if (enemies [i].enemyReachedIt == false) {
						for (int j = 0; j < enemyPressurePoint.Count; j++) {
							if (enemies [i].curX != enemyPressurePoint [j].plate.GetComponent<Node> ().x &&
							    enemies [i].curY != enemyPressurePoint [j].plate.GetComponent<Node> ().y) {
								if (enemyPressurePoint [j].IsPressureOn == true) {
									enemyPressurePoint [j].IsPressureOn = false;
									Debug.Log ("Pressure Point: " + enemyPressurePoint [j] + " status: " + enemyPressurePoint [j].IsPressureOn);
									enemyPressurePoint [j].ToBeReseted = true;
								}
							}
						}
					}
				}
			}

			if(IsComplete == true && InOrder == false)
			{
				for (int i = 0; i < originalPressurePoint.Count; i++) {
					if (player.GetComponent<Movement> ().curX == playerPressurePoint [i].plate.GetComponent<Node> ().x &&
						player.GetComponent<Movement> ().curY == playerPressurePoint [i].plate.GetComponent<Node> ().y) {
						IsComplete = true;
					}

					if (player.GetComponent<Movement> ().curX != playerPressurePoint [i].plate.GetComponent<Node> ().x &&
						player.GetComponent<Movement> ().curY != playerPressurePoint [i].plate.GetComponent<Node> ().y) {
						IsComplete = false;
					}
				}

				if (IsComplete == false) {
                    IsCorrect = false;
                    Reset ();
				}
			}


		}
	}

	void UpdatePlates()
	{
		if (originalPressurePoint.Count != 0 && originalPressurePoint != null) {
			foreach (PressurePoint go in originalPressurePoint) {
				
				if (go != null && go.IsPressureOn == false) {
					if (!playerPressurePoint.Contains(go) && !enemyPressurePoint.Contains(go)) {
                        go.plate.GetComponent<SpriteRenderer>().sprite = PressureTile;

						if (go.plateDown == true) {
							Vector3 current = new Vector3 (go.originalPosX, go.originalPosY);
							go.plate.GetComponent<Transform> ().transform.position = current;
							go.plateDown = false;
						}
					}

					if (go.ToBeReseted == true) {
                        go.plate.GetComponent<SpriteRenderer>().sprite = PressureTile;

                        if (go.plateDown == true) {
							Vector3 current = new Vector3 (go.originalPosX, go.originalPosY);
							go.plate.GetComponent<Transform> ().transform.position = current;
							go.ToBeReseted = false;
							go.plateDown = false;

							if (enemyPressurePoint.Contains (go)) {
								enemyPressurePoint.Remove (go);
							}
						}
					}
				}
			}
		}

		if (playerPressurePoint.Count != 0 && playerPressurePoint != null) {
			foreach (PressurePoint go in playerPressurePoint) {

				if (go != null && go.IsPressureOn == true) {
					go.plate.GetComponent<SpriteRenderer> ().color = Color.cyan;
					if (go.plateDown == false) {
						Vector3 target = new Vector3 (go.originalPosX, go.originalPosY - 0.05f);
						go.plate.GetComponent<Transform> ().transform.position = target;
						go.plateDown = true;
                    }
                }
			}
		}

		if (enemyPressurePoint.Count != 0 && enemyPressurePoint != null) {
			foreach (PressurePoint go in enemyPressurePoint) {

				if (go != null && go.IsPressureOn == true) {
					go.plate.GetComponent<SpriteRenderer> ().color = Color.cyan;

					if (go.plateDown == false) {
						Vector3 target = new Vector3 (go.originalPosX, go.originalPosY - 0.05f);
						go.plate.GetComponent<Transform> ().transform.position = target;
						go.plateDown = true;
                        //Sound here too, this is for enemy active pressure plate sound
                    }
                }

				/*
				if (go != null && go.IsPressureOn == false) {

					enemyPressurePoint.Remove (go);
				
					//if(go.plateDown)
					timer++;
					//Debug.Log (timer);
					if(timer * Time.deltaTime > 3)
					{
						Debug.Log ("Enemy tile reseted");
						enemyPressurePoint.Remove (go);
						if (!enemyPressurePoint.Contains (go)) 
						{
							Debug.Log ("Enemy tile removed");
						}
					}
				}*/
			}
		}
	}



	void CrossCheckPressurePoints()
	{
		if(originalPressurePoint.Count == playerPressurePoint.Count)
		{
			if(player.GetComponent<Movement> ().playerAtRest == true)
			{
                for (int i = 0; i < originalPressurePoint.Count; i++)
                {
                    if (originalPressurePoint[i].x == playerPressurePoint[i].x && originalPressurePoint[i].y == playerPressurePoint[i].y)
                    {
                        InOrderCount++;
                    }
                }
					if (InOrderCount == originalPressurePoint.Count)
					{
						InOrder = true;
						IsComplete = true;
                        IsCorrect = true;
                        Debug.Log("Solved");
					}
					if(InOrderCount<originalPressurePoint.Count)
					{
						InOrder = false;
                        IsCorrect = false;
						IsComplete = true;
                        Debug.Log("Not Solved");
                    sound.GetComponent<SoundManager>().PressureResetSource.Play();
					}
				}

			if(IsCorrect == false && InOrder == true && IsComplete == true)
			{
				Debug.Log ("Reset");
               // sound.GetComponent<SoundManager>().PressureResetSource.Play();
				InOrder = false;
			}else if(IsCorrect == true && InOrder == true && IsComplete == true)
            {
                Debug.Log("Opened");
                DoorCoordinates();
                sound.GetComponent<SoundManager>().PressureSolveSource.Play();
                InOrder = false;
            }
		}


	}

	void Reset()
	{
		foreach (PressurePoint go in playerPressurePoint) {
			if (go != null)
			{
				go.plate.GetComponent<SpriteRenderer> ().color = Color.red;

				if(go.plateDown == true) {
					Vector3 current = new Vector3 (go.originalPosX, go.originalPosY);
					float trueSpeed = 0.5f * Time.deltaTime;
					go.plate.GetComponent<Transform> ().transform.position = current;
					go.plateDown = false;
					go.IsPressureOn = false;
				}
			}
		}
		playerPressurePoint.Clear ();
		InOrderCount = 0;
	}

	public static void EnemyCoordinates()
	{
		enemies = new List<EnemyAIPatrol> (FindObjectsOfType (typeof(EnemyAIPatrol)) as EnemyAIPatrol[]);
	}

    public static void DoorCoordinates()
    {
        for (int i = 0; i < pHolder.childCount; i++)
        {
            if(pHolder.GetChild(i).transform.CompareTag("Statue"))
            {
                pHolder.GetChild(i).gameObject.SetActive(false);
                MapPlugin.Instance.FindBlock(pHolder.GetChild(i).GetComponent<Node>().x, pHolder.GetChild(i).GetComponent<Node>().y).layer = LayerMask.NameToLayer("Movable");
            }
        }
    }
}

/*
void CheckPressurePoints()
{
	if(originalPressurePoint != null && originalPressurePoint.Count > 0)
	{
		if(IsComplete == false && player.GetComponent<Movement> ().playerAtRest == true)
		{
			for (int i = 0; i < originalPressurePoint.Count; i++) {
				if (player.GetComponent<Movement> ().curX == originalPressurePoint [i].plate.GetComponent<Node> ().x &&
					player.GetComponent<Movement> ().curY == originalPressurePoint [i].plate.GetComponent<Node> ().y) {
					if (pressurePoint.Count < originalPressurePoint.Count + 1 && !pressurePoint.Contains (originalPressurePoint [i])) {
						pressurePoint.Add (originalPressurePoint [i]);
					}
					if (originalPressurePoint [i].IsPressureOn == false) {
						originalPressurePoint [i].IsPressureOn = true;
					}
				}

				if (enemies != null && enemies.Count > 0) {
					for (int j = 0; j < enemies.Count; j++) {
						if (enemies [j].curX == originalPressurePoint [i].plate.GetComponent<Node> ().x &&
							enemies [j].curY == originalPressurePoint [i].plate.GetComponent<Node> ().y) {
							if (originalPressurePoint [i].IsPressureOn == false) {
								originalPressurePoint [i].IsPressureOn = true;
							}
						} else {
							originalPressurePoint [i].IsPressureOn = false;
						}
					}
				}

				//ON hOLD
					if (enemies != null && enemies.Count > 0) {
						for (int j = 0; j < enemies.Count; j++) {
							if (enemies[j].curX != originalPressurePoint [i].plate.GetComponent<Node> ().x &&
								enemies[j].curY != originalPressurePoint [i].plate.GetComponent<Node> ().y) {
								if (originalPressurePoint [i].IsPressureOn == true) {
									timer++;
									if (timer * Time.deltaTime > 3) {
										originalPressurePoint [i].IsPressureOn = false;
										timer = 0;
									}
								}
							}
						}
					}
			}
		}

		if(IsComplete == true && InOrder == false)
		{
			for (int i = 0; i < originalPressurePoint.Count; i++) {
				if (player.GetComponent<Movement> ().curX == originalPressurePoint [i].plate.GetComponent<Node> ().x &&
					player.GetComponent<Movement> ().curY == originalPressurePoint [i].plate.GetComponent<Node> ().y) {
					IsComplete = true;
				}

				if (player.GetComponent<Movement> ().curX != originalPressurePoint [i].plate.GetComponent<Node> ().x &&
					player.GetComponent<Movement> ().curY != originalPressurePoint [i].plate.GetComponent<Node> ().y) {
					IsComplete = false;
				}
			}

			if (IsComplete == false) {
				pressurePoint.Clear ();
			}
		}
	}
}

void UpdatePlates()
{
	if (originalPressurePoint.Count != 0 && originalPressurePoint != null)
	{
		foreach (PressurePoint go in originalPressurePoint) {
			if (go != null && go.IsPressureOn == false)
			{
				if(!pressurePoint.Contains(go) || pressurePoint == null)
				{
					go.plate.GetComponent<SpriteRenderer> ().color = Color.red;

					if(go.plateDown == true)
					{
						Vector3 current = new Vector3 (go.originalPosX, go.originalPosY);
						Vector3 target = new Vector3 (go.originalPosX, go.originalPosY + 0.05f);
						float trueSpeed = 0.5f * Time.deltaTime;
						go.plate.GetComponent<Transform> ().transform.position = target;
						go.plateDown = false;
					}
				}
			}

			//ON hOLD
				if (enemies != null && enemies.Count > 0) {
					for (int j = 0; j < enemies.Count; j++) {
						if (enemies[j].curX != go.plate.GetComponent<Node> ().x &&
							enemies[j].curY != go.plate.GetComponent<Node> ().y) {
							if (go.IsPressureOn == true) {
								timer++;
								if (timer * Time.deltaTime > 3) {
									go.IsPressureOn = false;
									timer = 0;
								}
							}
						}
					}
				}

			if (go != null && go.IsPressureOn == true) {
				go.plate.GetComponent<SpriteRenderer> ().color = Color.magenta;
				if (go.plateDown == false) {
					Vector3 current = new Vector3 (go.originalPosX, go.originalPosY);
					Vector3 target = new Vector3 (go.originalPosX, go.originalPosY - 0.05f);
					float trueSpeed = 0.5f * Time.deltaTime;
					go.plate.GetComponent<Transform> ().transform.position = target;
					go.plateDown = true;
				}
			}
		}
	}
}
*/