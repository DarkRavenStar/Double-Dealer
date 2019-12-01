using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
	public int PlayerMoveLimit;
	public float playerSpeed;

	public int curX;
	public int curY;

	public List<GameObject> PlayerMovementTiles;
	public List<GameObject> AvailableMovementTiles;

	public GameObject LastMovementTileQueued;
	public GameObject tileMove;
	public GameObject onCurTile;
    public GameObject sound;

	int playerTile = 0;
	float targetPositionX;
	float targetPositionY;
	bool playerReachedIt = true;
	bool allowMovement = true;
	public bool playerAtRest = true;
	bool firstCoordinateEntered = false;
	public bool runOnce = false;
	public bool IsHiding = false;
	public bool IsStanding = true;
	public bool IsHidingLoopOnce = false;
	public bool playerEscaped = false;
	public bool IsPlayerEscaped = false;
	public bool HaveEscapedLoopOnce = false;
	int hidingCount = 0;

	public bool mouseBool = true;
	public bool SetMovement = false;
	public bool MovementOnPlayer = true;
	public bool inMiniGame = false;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void Update() {
        if (playerEscaped == true && SetMovement == false) {
			IsPlayerEscaped = true;
		}

		if(IsPlayerEscaped == false)
		{
			if (playerAtRest == true && MovementOnPlayer == true)
			{
				if(runOnce == true)
				{
                    if(onCurTile != null)
                    {
                        ClearAllPlayerTiles();
                        CalculateNextBlock(onCurTile.GetComponent<Collider2D>());
                        onCurTile.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                }
			}

			if (inMiniGame == false && runOnce == true) 
			{
				if (Input.GetMouseButton(0) && ScreenMouseRay() != null && SetMovement == false && MovementOnPlayer == true) 
				{
					playerAtRest = false;
					SetPlayerPath();
					UpdateMovementPath ();
				}
				else if(Input.GetMouseButtonUp(0))
				{
					if (PlayerMovementTiles.Count > 1) 
					{
						SetMovement = true;
					}
					else
					{
						SetMovement = false;
						playerAtRest = true;
						firstCoordinateEntered = false;
						playerTile = 0;
					}
				}

				if(SetMovement == true)
				{
					StartPlayerMovement();
				}
			}
		}
		PlayerAnimationChecking ();
    }

	void StartPlayerMovement()
	{
		if (PlayerMovementTiles [playerTile] != null)
		{
			if (playerReachedIt == true)
			{
				PlayerNavPointChecking();
				playerReachedIt = false;
				allowMovement = true;
			}

			if (playerReachedIt == false && allowMovement == true) 
			{
				PlayerMovement ();
			}
		} 
		else 
		{
			SetMovement = false;
			playerAtRest = true;
			firstCoordinateEntered = false;
			playerTile = 0;
			allowMovement = false;
			playerReachedIt = true;
		}

	}

	void SetPlayerPath() 
	{
		if (firstCoordinateEntered == false) 
		{
			LastMovementTileQueued = onCurTile;
			PlayerMovementTiles.Add (onCurTile);
			firstCoordinateEntered = true;
		}

		Collider2D[] col = ScreenMouseRay();
		foreach (Collider2D c in col) 
		{
			if (AvailableMovementTiles.Contains(c.gameObject))
			{
				if (PlayerMovementTiles.Count <= PlayerMoveLimit) 
				{
					LastMovementTileQueued = c.gameObject;
					PlayerMovementTiles.Add(c.gameObject);
					CalculateNextBlock(c);
					break;
				}
			}
		}
	}

	void PlayerAnimationChecking()
	{
		if(IsPlayerEscaped == false)
		{
			if (playerTile > 0 && playerTile < PlayerMovementTiles.Count && PlayerMovementTiles[playerTile] != null)
			{
				if(PlayerMovementTiles[playerTile].transform.position.x > PlayerMovementTiles[playerTile-1].transform.position.x 
					&& PlayerMovementTiles[playerTile].transform.position.y > PlayerMovementTiles[playerTile-1].transform.position.y)
				{
					PlayerAnimManager.Instance.DirectionReset ();
					PlayerAnimManager.forward = true;
				}

				if(PlayerMovementTiles[playerTile].transform.position.x < PlayerMovementTiles[playerTile-1].transform.position.x
					&& PlayerMovementTiles[playerTile].transform.position.y < PlayerMovementTiles[playerTile-1].transform.position.y)
				{
					PlayerAnimManager.Instance.DirectionReset ();
					PlayerAnimManager.backward = true;
				}

				if(PlayerMovementTiles[playerTile].transform.position.x < PlayerMovementTiles[playerTile-1].transform.position.x
					&& PlayerMovementTiles[playerTile].transform.position.y > PlayerMovementTiles[playerTile-1].transform.position.y)
				{
					PlayerAnimManager.Instance.DirectionReset ();
					PlayerAnimManager.left = true;
				}

				if(PlayerMovementTiles[playerTile].transform.position.x > PlayerMovementTiles[playerTile-1].transform.position.x
					&& PlayerMovementTiles[playerTile].transform.position.y < PlayerMovementTiles[playerTile-1].transform.position.y)
				{
					PlayerAnimManager.Instance.DirectionReset ();
					PlayerAnimManager.right = true;
				}

				if (playerReachedIt == false && allowMovement == true) 
				{
					PlayerAnimManager.Instance.ActionReset ();
					PlayerAnimManager.walk = true;
				}
			}

			if (playerAtRest == true && IsStanding == true) 
			{
				PlayerAnimManager.Instance.ActionReset ();
				PlayerAnimManager.stand = true;
			}

			if (playerAtRest == true && IsHiding == true) 
			{
				if (IsHidingLoopOnce == false) {
					PlayerAnimManager.Instance.ActionReset ();
					IsHidingLoopOnce = true;
					PlayerAnimManager.hide = true;
				}
			}
			else if (playerAtRest == true && IsHiding == true) 
			{
				PlayerAnimManager.hide = false;
			}
		}

		if (IsPlayerEscaped == true) 
		{
			if (HaveEscapedLoopOnce == false) {
				PlayerAnimManager.Instance.ActionReset ();
				PlayerAnimManager.Instance.DirectionReset ();
				PlayerAnimManager.escape = true;
				HaveEscapedLoopOnce = true;
			}
		}
	}

	void PlayerNavPointChecking()
	{
		if (IsPlayerEscaped == false) {
			playerTile++;
			if (playerTile < PlayerMovementTiles.Count && PlayerMovementTiles [playerTile] != null) {
				if (playerTile > 0) {
					onCurTile = PlayerMovementTiles [playerTile];
					SetXY (onCurTile.GetComponent<Node> ().x, onCurTile.GetComponent<Node> ().y);
					targetPositionX = PlayerMovementTiles [playerTile].transform.position.x;
					targetPositionY = PlayerMovementTiles [playerTile].transform.position.y;
					GetComponent<SpriteRenderer> ().sortingOrder = onCurTile.GetComponent<SpriteRenderer> ().sortingOrder;

					tileMove = PlayerMovementTiles [playerTile - 1];
					if (tileMove != null) {
						tileMove.GetComponent<SpriteRenderer> ().color = Color.white;
						sound.GetComponent<SoundManager> ().walkSource.Play ();
					}
				}
			}
		}

		if (playerTile == PlayerMovementTiles.Count)
		{
			SetMovement = false;
			playerAtRest = true;
			PlayerHidingChecking ();
			firstCoordinateEntered = false;
			playerTile = 0;
		}
    }

	void ClearAllPlayerTiles()
	{
		if (PlayerMovementTiles != null && AvailableMovementTiles != null) {
			if (PlayerMovementTiles.Count > 0) {
				foreach (GameObject go in AvailableMovementTiles) {
					if(go != null) go.GetComponent<SpriteRenderer> ().color = Color.white;
				}

				foreach (GameObject go in PlayerMovementTiles) {
					if(go != null) go.GetComponent<SpriteRenderer> ().color = Color.white;
				}

				AvailableMovementTiles.Clear ();
				PlayerMovementTiles.Clear ();
			}
		}
	}

	void PlayerMovement()
	{
		Vector3 target = new Vector3 (targetPositionX, targetPositionY);
		float trueSpeed = playerSpeed * Time.deltaTime;

		if (allowMovement == true)
		{
			transform.position = Vector3.MoveTowards(transform.position, target, trueSpeed);
		}

		if (target == transform.position) 
		{
			allowMovement = false;
			playerReachedIt = true;
		}
	}

	void CalculateNextBlock(Collider2D ClickedBlock) {
		if (PlayerMovementTiles != null) {
			if (MapPlugin.Instance.FindBlock (ClickedBlock) != null) {

				if (AvailableMovementTiles != null) {
					foreach (GameObject go in AvailableMovementTiles) {
						if(go != null) go.GetComponent<SpriteRenderer> ().color = Color.white;
					}
				}

				AvailableMovementTiles.Clear ();
				GameObject tempCTile = MapPlugin.Instance.FindBlock (ClickedBlock);
				Node TempTileXY = tempCTile.GetComponent<Node> ();

				if (PlayerMovementTiles.Count <= PlayerMoveLimit) {
					for (int i = -1; i < 2; i++) {
						for (int j = -1; j < 2; j++) {
							if (i != j) {
								if (i + j != 0) {
									if (MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j) != null &&
									   !PlayerMovementTiles.Contains (MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j)) &&
									   MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j).layer == LayerMask.NameToLayer ("Movable")) {
										AvailableMovementTiles.Add (MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j));
										MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j).GetComponent<SpriteRenderer> ().color = Color.yellow;
									}
								}
							}
						}
					}
				}
			}
		}
	}

    void SetXY(int tx, int ty) {
        curX = tx;
        curY = ty;
    }

	public void OnCurTileUpdate()
	{
		onCurTile = MapPlugin.Instance.FindBlock (MapPlugin.Instance.PlayerSpawnX, MapPlugin.Instance.PlayerSpawnY);
        LastMovementTileQueued = onCurTile;
        PlayerMovementTiles.Add(onCurTile);
		ClearAllPlayerTiles();
		CalculateNextBlock(onCurTile.GetComponent<Collider2D>());
		onCurTile.GetComponent<SpriteRenderer>().color = Color.blue;
        sound = GameObject.FindGameObjectWithTag("SoundManager");
        runOnce = true;
	}

	void PlayerHidingChecking ()
	{
		GameObject tempCTile = MapPlugin.Instance.FindBlock (onCurTile.GetComponent<Collider2D>());
		Node TempTileXY = tempCTile.GetComponent<Node> ();
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				if (i != j) {
					if (i + j != 0) {
						if (MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j) != null &&
						    !PlayerMovementTiles.Contains (MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j)) &&
						    MapPlugin.Instance.FindBlock (TempTileXY.x + i, TempTileXY.y + j).layer == LayerMask.NameToLayer ("Props")) {
							hidingCount++;
						}
					}
				}
			}
		}

		if (hidingCount > 0) 
		{
			IsHiding = true;
			IsStanding = false;
			IsHidingLoopOnce = false;
			hidingCount = 0;
		}
		else
		{
			IsHiding = false;
			IsStanding = true;
		}
	}

    void UpdateMovementPath() {
        if (PlayerMovementTiles.Count != 0) {
            foreach (GameObject go in PlayerMovementTiles) {
				if(go != null) go.GetComponent<SpriteRenderer>().color = Color.red;
            }
			if(LastMovementTileQueued != null) LastMovementTileQueued.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    Collider2D[] ScreenMouseRay() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 1f;

        Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);

        if (col.Length > 0) {
            return col;
        }

        return null;
    }
}



/* OLD MOVEMENT SCRIPT
 * void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        GetMovementPath();
        UpdateMovementPath();

        if (Input.GetMouseButtonUp(0) && CheapHax == true) {
            PlayerMovementTiles.Clear();
            CheapHax = false;
        }

        if (Input.GetKeyDown("return")) {
            Application.Quit();
        }

	}

    /*void NodeMove() {
        Node oNode = onCurTile.GetComponent<Node>();
        for (int i = 0; i < TileNode.Count; i++) {
            Node tNode = TileNode[i].GetComponent<Node>();
            if (TileNode[i] != onCurTile) {
                if (tNode.fCost < oNode.fCost || (tNode.fCost == oNode.fCost && tNode.hCost < oNode.hCost)) {
                    this.transform.position = TileNode[i].transform.position;
                    onCurTile = TileNode[i];
                    break;
                }
            }
        }
    }*/

/*void GetMovementPath() {
    if(inMiniGame == false)
    {
        if (Input.GetMouseButton(0) && ScreenMouseRay() != null) {
            Collider2D[] col = ScreenMouseRay();
            foreach (Collider2D c in col) {
                if (c.gameObject == onCurTile) {
                    MovementOnPlayer = true;
                }

                if (c.transform.gameObject.layer == LayerMask.NameToLayer("Movable") && !(PlayerMovementTiles.Contains(c.gameObject)) && MovementOnPlayer == true && CheapHax == false) {
                    PlayerMovementTiles.Enqueue(c.gameObject);
                    LastMovementTileQueued = c.gameObject;
                }
            }
        } else if (ScreenMouseRay() == null && MovementOnPlayer == true) {
            ClearColorTiles();
            MovementOnPlayer = false;
            CheapHax = true;
        } else if (ScreenMouseRay() != null && MovementOnPlayer == true) {
            Collider2D[] col = ScreenMouseRay();
            foreach (Collider2D c in col) {
                if (c.gameObject.layer == LayerMask.NameToLayer("Props")) {
                    ClearColorTiles();
                    MovementOnPlayer = false;
                    CheapHax = true;
                    break;
                }
            }
        }

        if(CheapHax == false) {
            StartCoroutine(StartMovement());
        }
    }
}

void ClearColorTiles() {
    while (PlayerMovementTiles.Count != 0) {
        PlayerMovementTiles.Dequeue().GetComponent<SpriteRenderer>().color = Color.white;
    }
}

/*IEnumerator StartMovement() {
    if (Input.GetMouseButtonUp(0)) {
        while (PlayerMovementTiles.Count != 0) {
            MovementOnPlayer = false;
            tileMove = PlayerMovementTiles.Dequeue();
            tileMove.GetComponent<SpriteRenderer>().color = Color.white;
            onCurTile = tileMove;
            while (transform.position != tileMove.transform.position + new Vector3(0, 0.25f, 0)) {
                transform.position = Vector2.MoveTowards(transform.position, tileMove.transform.position + new Vector3 (0, 0.25f, 0), speed * Time.deltaTime);
                SetXY(onCurTile.GetComponent<Node>().x, onCurTile.GetComponent<Node>().y);
            }
            yield return new WaitForSeconds(0.05f);
        }
        onCurTile.GetComponent<SpriteRenderer>().color = Color.black;
    }
}*/
