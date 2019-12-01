using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimManager : MonoBehaviour {

	Animator anim;
	//General direction the character is facing
	public static bool forward;
	public static bool backward;
	public static bool left;
	public static bool right;

	//Action
	public static bool walk;
	public static bool stand;
	public static bool hide;
	public static bool hack;
	public static bool idle;
	public static bool escape;

	bool playerDeath = false;
	//Timing
	float hideTiming = 0.75f;
	float hackTiming;
	float smokeBomb;

	float hideAnimTimer = 0;
	float deathAnimTimer = 0;
	bool isHiding = false;
	bool IsEscape = false;

    public GameObject sound;
	private static PlayerAnimManager _instance;

	public static PlayerAnimManager Instance
	{
		get 
		{
			return _instance;
		}
	}

	void Awake()
	{
		if (_instance != null && _instance != this) 
		{
			Destroy(this.gameObject);
		} 
		else 
		{
			DontDestroyOnLoad(gameObject);
			_instance = this;
		}
	}

	void Start ()
	{
		anim = this.GetComponent<Animator>();
        sound = GameObject.Find("SoundManager");
	}

	void Update ()
	{
		PlayerAnimation ();
	}

	void PlayerAnimation()
	{
		if(escape == true)
		{
			if (IsEscape == false && deathAnimTimer * Time.deltaTime < 1.5f) {

				anim.Play ("SmokeBomb");
                SoundManager.Instance.bombSource.Play();
				deathAnimTimer++;
				if (deathAnimTimer * Time.deltaTime >= 1.5f) {
					IsEscape = true;
				}
			}
			
			if (IsEscape == true && deathAnimTimer * Time.deltaTime >= 1.5f) {
				escape = false;
				IsEscape = false;
				deathAnimTimer = 0;
				forward = true;
				stand = true;
                EndDetect.isCodeAvailable = false;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SoundManager.Instance.GameOverSource.Play();
				Destroy(this.gameObject);
				//SceneManager.LoadScene(4);
				//gameObject.SetActive (false);
			}
		}
		if (walk == true && forward == true)
		{
			anim.Play ("Walk_B_R");
		}
		else if (stand == true && forward == true)
		{
			anim.Play ("Stand_B_R");
		}
		else if (idle == true && forward == true)
		{
			anim.Play ("Idle_B_R");
		}
		else if (hack == true && forward == true)
		{
			anim.Play ("Hacking_B_R");
		}
		else if (hide == true && forward == true)
		{
			if (isHiding == false && hideAnimTimer <= hideTiming) {
				anim.Play ("Hiding_B_R");
				isHiding = true;
				hideAnimTimer++;
			}

			if(isHiding = true && hideAnimTimer > hideTiming) {
				anim.Play ("Hiding_B_R_Head");
			}
		}

		if (walk == true && backward == true)
		{
			anim.Play("Walk_F_L");

		}
		else if (stand == true && backward == true)
		{
			anim.Play("Stand_F_L");
		}
		else if (idle == true && backward == true)
		{
			anim.Play("Idle_F_L");
		}
		else if (hack == true && backward == true)
		{
			anim.Play("Hacking_F_L");
		}
		else if (hide == true && backward == true)
		{
			if (isHiding == false && hideAnimTimer <= hideTiming) {
				anim.Play ("Hiding_F_L");;
				isHiding = true;
				hideAnimTimer++;
			}

			if(isHiding = true && hideAnimTimer > hideTiming) {
				anim.Play ("Hiding_F_L_Head");
			}
		}

		if (walk == true && left == true)
		{
			anim.Play("Walk_B_L");
		}
		else if (stand == true && left == true)
		{
			anim.Play("Stand_B_L");
		}
		else if (idle == true && left == true)
		{
			anim.Play("Idle_B_L");
		}
		else if (hack == true && left == true)
		{
			anim.Play("Hacking_B_L");
		}
		else if (hide == true && left == true)
		{
			if (isHiding == false && hideAnimTimer <= hideTiming) {
				anim.Play ("Hiding_B_L");
				isHiding = true;
				hideAnimTimer++;
			}

			if(isHiding = true && hideAnimTimer > hideTiming) {
				anim.Play ("Hiding_B_L_Head");
			}
		}

		if (walk == true && right == true)
		{
			anim.Play("Walk_F_R");
		}
		else if (stand == true && right == true)
		{
			anim.Play("Stand_F_R");
		}
		else if (idle == true && right == true)
		{
			anim.Play("Idle_F_R");
		}
		else if (hack == true && right == true)
		{
			anim.Play("Hacking_F_R");
		}
		else if (hide == true && right == true)
		{
			if (isHiding == false && hideAnimTimer <= hideTiming) {
				anim.Play ("Hiding_F_R");
				isHiding = true;
				hideAnimTimer++;
			}

			if(isHiding = true && hideAnimTimer > hideTiming) {
				anim.Play ("Hiding_F_R_Head");
			}
		}

		if (hide == false) 
		{
			isHiding = false;
			hideAnimTimer = 0;
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
