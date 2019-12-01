using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerActionKeyBased ();
	}

	void PlayerActionKeyBased()
	{
		/*
		if(Input.GetKeyDown(KeyCode.W))
		{
			PlayerAnimManager.Instance.PlayerAction ("forward", true, "walk", true);
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			PlayerAnimManager.Instance.PlayerAction ("forward", true, "stand", true);
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			PlayerAnimManager.Instance.PlayerAction ("backward", true, "walk", true);
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			PlayerAnimManager.Instance.PlayerAction ("backward", true, "stand", true);
		}


		if (Input.GetKeyDown(KeyCode.D))
		{
			PlayerAnimManager.Instance.PlayerAction ("right", true, "walk", true);
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			PlayerAnimManager.Instance.PlayerAction ("right", true, "stand", true);
		}


		if (Input.GetKeyDown(KeyCode.A))
		{
			PlayerAnimManager.Instance.PlayerAction ("left", true, "walk", true);
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			PlayerAnimManager.Instance.PlayerAction ("left", true, "stand", true);
		}


		if (Input.GetKeyDown(KeyCode.E))
		{
			PlayerAnimManager.Instance.PlayerAction ("left", true, "hack", true);
		}
		if (Input.GetKeyUp(KeyCode.E))
		{
			PlayerAnimManager.Instance.PlayerAction ("left", true, "stand", true);
		}*/

		//MoveForward
		if(Input.GetKeyDown(KeyCode.W))
		{
			PlayerAnimManager.forward = true;
			PlayerAnimManager.walk = true;
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			PlayerAnimManager.forward = true;
			PlayerAnimManager.stand = true;
		}

		//MoveBackward
		if (Input.GetKeyDown(KeyCode.S))
		{
			PlayerAnimManager.backward = true;
			PlayerAnimManager.walk = true;
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			PlayerAnimManager.backward = true;
			PlayerAnimManager.stand = true;
		}

		//MoveRight
		if (Input.GetKeyDown(KeyCode.D))
		{
			PlayerAnimManager.right = true;
			PlayerAnimManager.walk = true;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			PlayerAnimManager.right = true;
			PlayerAnimManager.stand = true;
		}

		//MoveLeft
		if (Input.GetKeyDown(KeyCode.A))
		{
			PlayerAnimManager.left = true;
			PlayerAnimManager.walk = true;
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			PlayerAnimManager.left = true;
			PlayerAnimManager.stand = true;
		}

		//HidingLeft
		if (Input.GetKeyDown(KeyCode.Z))
		{
			PlayerAnimManager.left = true;
			PlayerAnimManager.hide = true;
		}
		if (Input.GetKeyUp(KeyCode.Z))
		{
			PlayerAnimManager.left = true;
			PlayerAnimManager.stand = true;
		}

		//HidingRight
		if (Input.GetKeyDown(KeyCode.X))
		{
			PlayerAnimManager.right = true;
			PlayerAnimManager.hide = true;
		}
		if (Input.GetKeyUp(KeyCode.X))
		{
			PlayerAnimManager.right = true;
			PlayerAnimManager.stand = true;
		}

		//HidingBackward
		if (Input.GetKeyDown(KeyCode.C))
		{
			PlayerAnimManager.backward = true;
			PlayerAnimManager.hide = true;
		}
		if (Input.GetKeyUp(KeyCode.C))
		{
			PlayerAnimManager.backward = true;
			PlayerAnimManager.stand = true;
		}

		//HidingForward
		if (Input.GetKeyDown(KeyCode.V))
		{
			PlayerAnimManager.forward = true;
			PlayerAnimManager.hide = true;
		}
		if (Input.GetKeyUp(KeyCode.V))
		{
			PlayerAnimManager.forward = true;
			PlayerAnimManager.stand = true;
		}


		//HackingLeft
		if (Input.GetKeyDown(KeyCode.B))
		{
			PlayerAnimManager.left = true;
			PlayerAnimManager.hack = true;
		}
		if (Input.GetKeyUp(KeyCode.B))
		{
			PlayerAnimManager.left = true;
			PlayerAnimManager.stand = true;
		}

		//HackForward
		if (Input.GetKeyDown(KeyCode.N))
		{
			PlayerAnimManager.forward = true;
			PlayerAnimManager.hack = true;
		}
		if (Input.GetKeyUp(KeyCode.N))
		{
			PlayerAnimManager.forward = true;
			PlayerAnimManager.stand = true;
		}

		/*
		if (Input.GetKeyDown(KeyCode.N))
		{
			
		}
		if (Input.GetKeyUp(KeyCode.N))
		{
			
		}
		*/
	}
}
