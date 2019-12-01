using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyType;

public class EnemyAnimManager : MonoBehaviour {

	Animator anim;
	//General direction the character is facing
	public bool forward;
	public bool backward;
	public bool left;
	public bool right;

	//Moving
	public bool walk;
	public bool run;
	public bool stand;
	public bool hide;
	public bool hack;
	public bool idle;

	void Start ()
	{
		anim = this.GetComponent<Animator>();
	}

	void Update ()
	{
		EnemyAnimation ();
	}

	void EnemyAnimation()
	{
		if (walk == true && forward == true)
		{
			anim.Play ("Walk_B_R");
		}
		else if (stand == true && forward == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Watch_B_R");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Idle_B_R");
			}
		}
		else if (run == true && forward == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Run_B_R");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Walk_B_R");
			}
		}

		if (walk == true && backward == true)
		{
			anim.Play("Walk_F_L");
		}
		else if (stand == true && backward == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Watch_F_L");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Idle_F_L");
			}
		}
		else if (run == true && backward == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Run_F_L");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Walk_F_L");
			}
		}

		if (walk == true && left == true)
		{
			anim.Play("Walk_B_L");
		}
		else if (stand == true && left == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Watch_B_L");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Idle_B_L");
			}
		}
		else if (run == true && left == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Run_B_L");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Walk_B_L");
			}
		}

		if (walk == true && right == true)
		{
			anim.Play("Walk_F_R");
		}
		else if (stand == true && right == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Watch_F_R");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Idle_F_R");
			}
		}
		else if (run == true && right == true)
		{
			if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.ROBOT_GUARD)
			{
				anim.Play("Run_F_R");
			}
			else if(GetComponent<EnemyDetectionTile>().enemyType == ENEMY_TYPE.HUMAN_GUARD)
			{
				anim.Play("Walk_F_R");
			}
		}
	}

	public void DirectionReset()
	{
		forward = false;
		backward = false;
		left = false;
		right = false;
	}

	public void ActionReset()
	{
		walk = false;
		stand = false;
		idle = false;
		hack = false;
		hide = false;
	}

	public void AnimationBooleanClear()
	{
		forward = false;
		backward = false;
		left = false;
		right = false;

		walk = false;
		stand = false;
		idle = false;
		hack = false;
	}
}
