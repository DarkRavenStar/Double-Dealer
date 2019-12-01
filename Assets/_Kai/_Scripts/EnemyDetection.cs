using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

    public int curX;
    public int curY;

    public bool PlayerDetected = false;

    public GameObject Player;

    private void Start() {
        Node enePos = GetComponent<Node>();
        Player = GameObject.Find("Player");
        curX = enePos.x;
        curY = enePos.y;
    }

    private void Update() {
        ShowPickpocketRadius();
        Detection();
    }

    void Detection() {
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                if (i != j) {
                    if (i + j != 0) {
                        if (MapPlugin.Instance.FindBlock(curX + i, curY + j) != null) {
                            //MapPlugin.Instance.FindBlock(TempTileXY.x + i, TempTileXY.y + j).layer == LayerMask.NameToLayer("Movable")
                            GameObject eneBlock = MapPlugin.Instance.FindBlock(curX + i, curY + j);
                            if (Player.GetComponent<Movement>().curX == curX + i && Player.GetComponent<Movement>().curY == curY + j) {
                                PlayerDetected = true;
								Debug.Log("Player Detected");
                            }
                        }
                    }
                }
            }
        }

        if ((Player.GetComponent<Movement>().curX != curX + 1 && Player.GetComponent<Movement>().curY != curY) &&
            (Player.GetComponent<Movement>().curX != curX - 1 && Player.GetComponent<Movement>().curY != curY) &&
            (Player.GetComponent<Movement>().curX != curX && Player.GetComponent<Movement>().curY != curY + 1) &&
            (Player.GetComponent<Movement>().curX != curX && Player.GetComponent<Movement>().curY != curY - 1)) {
            PlayerDetected = false;
			Debug.Log("Player Not Here");
        }
    }

    void ShowPickpocketRadius() {
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                if (i != j) {
                    if (i + j != 0) {
                        if (MapPlugin.Instance.FindBlock(curX + i, curY + j) != null) {
                            //MapPlugin.Instance.FindBlock(TempTileXY.x + i, TempTileXY.y + j).layer == LayerMask.NameToLayer("Movable")
                            GameObject eneBlock = MapPlugin.Instance.FindBlock(curX + i, curY + j);
                            if (eneBlock.layer == LayerMask.NameToLayer("Movable")) {
                                eneBlock.GetComponent<SpriteRenderer>().color = Color.red;
                            }
                         }
                    }
                }
            }
        }
    }
}
