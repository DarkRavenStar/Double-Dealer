using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ExecuteZone {
	public int x = -1;
	public int y = -1;

	public ExecuteZone(int tx, int ty) {
		x = tx;
		y = ty;
	}

	public void SetXY(int tx, int ty) {
		x = tx;
		y = ty;
	}
}

public class PropAction : MonoBehaviour {

	[System.Serializable]
	public enum MINIGAMETYPE {
		NULL = -1,
		WALL_PANEL_HACK,
		PIPE_HACK,
		SWITCH_SCENE,
		ACTIVATE_DOOR_KEYCARD,
		ACTIVATE_DOOR,
		ACTIVATE_DOOR_PUZZLE,
		END_GAME
	}

	public GameObject obj;
	public GameObject MiniGame;
	public GameObject plyr;

	public bool TutorialDoor = false;
	public bool isInitialized = true;
	public bool Activated = false;
	public bool MiniGameDone = false;
	public GameObject sound;
	public ExecuteZone curZone;
	public ExecuteZone tempZone;

	public MINIGAMETYPE MinigameType;

	public List<ExecuteZone> Zone;

	private void Awake() {
		plyr = GameObject.Find("Player");
	}

	private void Start() {
		if (MinigameType == MINIGAMETYPE.SWITCH_SCENE) {
			MapPlugin.Instance.FindBlock(GetComponent<Node>().x, GetComponent<Node>().y).GetComponent<SpriteRenderer>().color = Color.white;
			MapPlugin.Instance.FindBlock(GetComponent<Node>().x, GetComponent<Node>().y).layer = LayerMask.NameToLayer("Movable");
		}
		sound = GameObject.Find("SoundManager");

		if (MinigameType == MINIGAMETYPE.WALL_PANEL_HACK)
		{
			MiniGame = GameObject.Find("NumberPadBG").gameObject;
			MiniGame.SetActive(false);
		}
		else if (MinigameType == MINIGAMETYPE.PIPE_HACK)
		{
			MiniGame = GameObject.Find("PipeGameCanvas").gameObject;
			//MiniGame.SetActive(false);
			MiniGame.transform.GetChild(0).gameObject.SetActive(false);
			MiniGame.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	private void OnMouseDown() {
		if (MinigameType == MINIGAMETYPE.SWITCH_SCENE) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			sound.GetComponent<SoundManager> ().DoorOpenSource.Play ();
			plyr.GetComponent<Transform> ().position = new Vector3 (100f, 100f, 0f);
		} else if (MinigameType == MINIGAMETYPE.END_GAME && GetComponent<Door> ().canOpen == true) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 2);
			SoundManager.Instance.WinSource.Play ();
		} else if (MinigameType == MINIGAMETYPE.ACTIVATE_DOOR_KEYCARD && Activated == false && GetComponent<Door> ().canOpen == true) {
			if (InventoryManager.Instance.SearchItem (InventoryList.Inventory.IITEM.KEYCARD) != -1) {
				int IItemSlot = InventoryManager.Instance.SearchItem (InventoryList.Inventory.IITEM.KEYCARD);
				//GameObject.FindWithTag ("Canvas").transform.GetChild (0).GetChild (IItemSlot).GetComponent<InventoryItem> ().ClearSprite ();
				InventoryManager.Instance.playerItems.RemoveAt (IItemSlot);

				GameObject PropTile = MapPlugin.Instance.FindBlock (GetComponent<Node> ().x, GetComponent<Node> ().y);
				PropTile.GetComponent<SpriteRenderer> ().color = Color.white;
				PropTile.layer = LayerMask.NameToLayer ("Movable");
				Activated = true;
				GetComponent<DoorOpenUI> ().canOpen = true;
				gameObject.SetActive (false);

				if (TutorialDoor == true) {
					SoundManager.Instance.DoorOpenSource.Play ();
					GetComponent<DialogueTrigger> ().TriggerDialogue ();
					TutorialDoor = false;
				}
				GetComponent<DoorDetectionTile> ().ClearDetectionPath ();
			} else {
				SoundManager.Instance.DoorNotOpenSource.Play ();
				GetComponent<DoorOpenUI> ().cannotOpen = true;
			}
		} else if (MinigameType == MINIGAMETYPE.ACTIVATE_DOOR && Activated == false && GetComponent<Door> ().canOpen == true) {
			GameObject PropTile = MapPlugin.Instance.FindBlock (GetComponent<Node> ().x, GetComponent<Node> ().y);
			PropTile.GetComponent<SpriteRenderer> ().color = Color.white;
			PropTile.layer = LayerMask.NameToLayer ("Movable");
			Activated = true;
			GetComponent<DoorDetectionTile> ().ClearDetectionPath ();
			GetComponent<DoorOpenUI> ().canOpen = true;
			this.gameObject.SetActive (false);
		} else if (MinigameType == MINIGAMETYPE.WALL_PANEL_HACK || MinigameType == MINIGAMETYPE.PIPE_HACK) {
			if (MiniGameDone == false && GetComponent<Door> ().canOpen == true) {
				if (MinigameType == MINIGAMETYPE.WALL_PANEL_HACK) {
					MiniGame.SetActive (true);
					MiniGame.GetComponentInChildren<EnterControl> ().propAction = this.gameObject;
				}
			}

            if (MinigameType == MINIGAMETYPE.PIPE_HACK) {
                MiniGame.transform.GetChild(0).gameObject.SetActive(true);
                MiniGame.GetComponentInChildren<EndDetect>().item = this.gameObject;
            }
            plyr.GetComponent<Movement>().inMiniGame = true;
            if(MiniGameDone == true)
            {
                if(MinigameType == MINIGAMETYPE.WALL_PANEL_HACK)
                {
                    plyr.GetComponent<Movement>().inMiniGame = false;
                }
            }
        }
	}
}
