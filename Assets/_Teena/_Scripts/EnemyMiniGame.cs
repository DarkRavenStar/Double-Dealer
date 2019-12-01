using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMiniGame : MonoBehaviour {

	[System.Serializable]
	public enum MINIGAMETYPE {
		NULL = 0,
		WALL_PANEL_HACK,
		PIPE_HACK,
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
		if (MinigameType == MINIGAMETYPE.WALL_PANEL_HACK)
		{
			MiniGame = GameObject.Find("NumberPadBG").gameObject;
			MiniGame.SetActive(false);
		}
		else if (MinigameType == MINIGAMETYPE.PIPE_HACK)
		{
			MiniGame = GameObject.Find("PipeGameCanvas").gameObject;
			MiniGame.transform.GetChild(0).gameObject.SetActive(false);
			MiniGame.transform.GetChild(1).gameObject.SetActive(false);
		}
	}


	private void OnMouseDown() {
		if (MinigameType == MINIGAMETYPE.WALL_PANEL_HACK || MinigameType == MINIGAMETYPE.PIPE_HACK) {
			if (MiniGameDone == false && GetComponent<Enemy> ().canBePickpocketed == true) {
				if (MinigameType == MINIGAMETYPE.WALL_PANEL_HACK) {
					MiniGame.SetActive (true);
					MiniGame.GetComponentInChildren<EnterControl> ().propAction = this.gameObject;
				}

				if (MinigameType == MINIGAMETYPE.PIPE_HACK) {
					MiniGame.transform.GetChild(0).gameObject.SetActive(true);
					MiniGame.GetComponentInChildren<EndDetect> ().item = this.gameObject;
				}
				plyr.GetComponent<Movement> ().inMiniGame = true;
			}
		}
	}
}
