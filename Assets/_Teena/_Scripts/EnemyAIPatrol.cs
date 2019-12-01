using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class NavPoint {
	public Transform navPoint;
	public float stopTiming;

	public NavPoint(Transform navP, float stopT)
	{
		navPoint = navP;
		stopTiming = stopT;
	}
};

public class EnemyAIPatrol : MonoBehaviour {

	public Transform startingPoint;
	public List<NavPoint> navPointList;

	EnemyAnimManager anim;

	public float speed;
	public float attackSpeed;
	float tempSpeed;

	Transform enemy;
	EnemyDetectionTile enemyTile;

	public GameObject previousTile;
	public GameObject onCurTile;

	public int curX;
	public int curY;

	int navObject = 0;
	int navPointMinus;

	public bool canReverse = false;
	public bool fullRound = false;
	public bool enemyReachedIt = true;
	public bool canEnemyStart = false;

	bool reversePatrol = false;
	bool allowMovement = true;
	//public bool IsNormalState = true;
	public bool IsAttackState = false;
	bool runOnce = false;
	bool runAttack = false;

	float timer = 0;
	public float tempStopTiming = 0;

	float targetPositionX;
	float targetPositionY;

    public bool BedRoomGuard = false;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<EnemyAnimManager> ();
		enemy = GetComponent<Transform>();
		enemyTile = GetComponent<EnemyDetectionTile> ();
	}

	// Update is called once per frame
	void Update()
	{
		if (runOnce == false) {
			if (navPointList.Count > 0) {
				navObject = 1;
				navPointMinus = 1;
				EnemyAnimationChecking ();
				enemyTile.CanSetDetectionPath = true;
				navObject = 0;
				navPointMinus = 0;
			} else if (navPointList.Count == 0) {
				anim.backward = true;
				enemyTile.CanSetDetectionPath = true;
			}

			runOnce = true;
		}

		if (canEnemyStart == true && runOnce == true)
		{
			if (IsAttackState == false) {
				tempSpeed = speed;
				Debug.Log ("Normal");
				if(navPointList [navObject].stopTiming != null)
				{
					tempStopTiming = navPointList [navObject].stopTiming;
				}
			}

			if(IsAttackState == true) {
				tempSpeed = attackSpeed;
				Debug.Log ("Attack");
				tempStopTiming = 0;
			}

			if (enemyReachedIt == true)
			{
				timer++;
				if(timer * Time.deltaTime > tempStopTiming)
				{
					enemyReachedIt = false;
					allowMovement = true;
					timer = 0;
					EnemyNavPointChecking();
				}
			}

			if (enemyReachedIt == false && allowMovement == true) 
			{
				EnemyMovement ();
			}

			EnemyAnimationChecking ();
		}
	}

	void EnemyNavPointChecking()
	{
		if (fullRound == false && canReverse == true)
		{
			if (navObject < navPointList.Count-1 && reversePatrol == false)
			{
				navObject++;
				targetPositionX = navPointList[navObject].navPoint.position.x;
				targetPositionY = navPointList[navObject].navPoint.position.y;

				onCurTile = navPointList [navObject].navPoint.gameObject;
				previousTile = navPointList [navObject-1].navPoint.gameObject;

				SetXY(onCurTile.GetComponent<Node>().x, onCurTile.GetComponent<Node>().y);
				enemyTile.CanSetDetectionPath = true;
				GetComponent<SpriteRenderer> ().sortingOrder = onCurTile.GetComponent<SpriteRenderer> ().sortingOrder;

				navPointMinus = 1;
			}

			if (navObject > 0 && reversePatrol == true)
			{
				navObject--;
				targetPositionX = navPointList[navObject].navPoint.position.x;
				targetPositionY = navPointList[navObject].navPoint.position.y;

				onCurTile = navPointList [navObject].navPoint.gameObject;
				previousTile = navPointList [navObject+1].navPoint.gameObject;

				SetXY(onCurTile.GetComponent<Node>().x, onCurTile.GetComponent<Node>().y);
				enemyTile.CanSetDetectionPath = true;
				GetComponent<SpriteRenderer> ().sortingOrder = onCurTile.GetComponent<SpriteRenderer> ().sortingOrder;

				navPointMinus = -1;
			}

		}

		if (fullRound == true && canReverse == false)
		{
			if (navObject < navPointList.Count) {
				navObject++;
				if (navObject > 0 && navObject < navPointList.Count) {
					targetPositionX = navPointList [navObject].navPoint.position.x;
					targetPositionY = navPointList [navObject].navPoint.position.y;

					onCurTile = navPointList [navObject].navPoint.gameObject;
					previousTile = navPointList [navObject - 1].navPoint.gameObject;

					SetXY (onCurTile.GetComponent<Node> ().x, onCurTile.GetComponent<Node> ().y);
					enemyTile.CanSetDetectionPath = true;
					GetComponent<SpriteRenderer> ().sortingOrder = onCurTile.GetComponent<SpriteRenderer> ().sortingOrder;
					navPointMinus = 1;
				}

				if (navObject == navPointList.Count) {
					targetPositionX = navPointList [0].navPoint.position.x;
					targetPositionY = navPointList [0].navPoint.position.y;

					onCurTile = navPointList [0].navPoint.gameObject;
					previousTile = navPointList [navPointList.Count - 1].navPoint.gameObject;

					SetXY (onCurTile.GetComponent<Node> ().x, onCurTile.GetComponent<Node> ().y);
					enemyTile.CanSetDetectionPath = true;
					GetComponent<SpriteRenderer> ().sortingOrder = onCurTile.GetComponent<SpriteRenderer> ().sortingOrder;
					navObject = 0;
					navPointMinus = -(navPointList.Count - 1);
				}
			}
		}


		if(onCurTile != null && previousTile != null)
		{
			previousTile.layer = LayerMask.NameToLayer("Movable");
			onCurTile.layer = LayerMask.NameToLayer("Enemy");
		}
	}

	void EnemyAnimationChecking()
	{
		if (navObject >= 0 && navObject < navPointList.Count)
		{
			if(navPointList[navObject].navPoint.position.x > navPointList[navObject-navPointMinus].navPoint.position.x 
				&& navPointList[navObject].navPoint.position.y > navPointList[navObject-navPointMinus].navPoint.position.y)
			{
				anim.DirectionReset ();
				anim.forward = true;
			}

			if(navPointList[navObject].navPoint.position.x < navPointList[navObject-navPointMinus].navPoint.position.x
				&& navPointList[navObject].navPoint.position.y < navPointList[navObject-navPointMinus].navPoint.position.y)
			{
				anim.DirectionReset ();
				anim.backward = true;
			}

			if(navPointList[navObject].navPoint.position.x < navPointList[navObject-navPointMinus].navPoint.position.x
				&& navPointList[navObject].navPoint.position.y > navPointList[navObject-navPointMinus].navPoint.position.y)
			{
				anim.DirectionReset ();
				anim.left = true;
			}

			if(navPointList[navObject].navPoint.position.x > navPointList[navObject-navPointMinus].navPoint.position.x
				&& navPointList[navObject].navPoint.position.y < navPointList[navObject-navPointMinus].navPoint.position.y)
			{
				anim.DirectionReset ();
				anim.right = true;
			}

			if (enemyReachedIt == false && allowMovement == true && IsAttackState == false) 
			{
				anim.ActionReset ();
				anim.walk = true;
			}

			if (enemyReachedIt == false && allowMovement == true && IsAttackState == true) 
			{
				if (runAttack == false) 
				{
					anim.ActionReset ();
					anim.run = true;
					runAttack = true;
				}
			}

			if (enemyReachedIt == true && allowMovement == false && IsAttackState == false) 
			{
				anim.ActionReset ();
				anim.stand = true;
			}
		}
	}

	void EnemyMovement()
	{
		Vector3 target = new Vector3 (targetPositionX, targetPositionY);
		float trueSpeed = tempSpeed * Time.deltaTime;

		if (allowMovement == true)
		{
			enemy.position = Vector3.MoveTowards(enemy.position, target, trueSpeed);
		}

		if (target == enemy.position) 
		{
			if (canReverse == true && fullRound == false)
			{
				if (navObject == navPointList.Count-1)
				{
					navObject = navPointList.Count-1;
					reversePatrol = true;
				}

				if (navObject == 0 && reversePatrol == true)
				{
					reversePatrol = false;
				}
			}

			allowMovement = false;
			enemyReachedIt = true;
		}
	}

	public void SetXY(int tx, int ty) {
		curX = tx;
		curY = ty;
	}
}
