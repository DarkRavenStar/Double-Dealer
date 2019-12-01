using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using EnemyType;

[System.Serializable]
public class Row {
    [HideInInspector]
    public string Name;

    public int[] typeBlock;
    public int[] amount;
    public int[] area;
};

[System.Serializable]
public class PropData {
    public GameObject item;
    public int x;
    public int y;


    public bool IsEmpty;
    //public bool customLayer;
    //public LAYER_TYPE Layer;
    public bool customOrderInLayer;
    public int orderInLayer = 0;
    public bool customZAxis;
    public int cZAxis = 0;
    public bool customState;
    public PropAction.MINIGAMETYPE State;
};

[System.Serializable]
public class Prop {
    public string name;
    public GameObject item;

    public bool IsTransparent;
    public bool customLayer;
    public LAYER_TYPE Layer;
    public bool customOrderInLayer;
    public int orderInLayer = 0;
    public PropAction.MINIGAMETYPE State;

    public PropData[] itemData;
};

[System.Serializable]
public class Navigation {
    public int x;
    public int y;
    public float stopTiming;
}

[System.Serializable]
public enum LAYER_TYPE {
    MOVABLE = 0,
    PROPS,
    INACTIVE,
    WALL,
    PLAYER,
    ENEMY,
    INTERACT,
    HACKABLE
}

namespace EnemyType {
    [System.Serializable]
    public enum ENEMY_TYPE {
        SLEEPING = 0,
        ROBOT_GUARD,
        HUMAN_GUARD,
        SUPER_GUARD, //A Higher level enemy that uses pyramid type detection [Not Implemented]
        CUSTOM,
    }
}

[System.Serializable]
public class EnemyAI {
    public GameObject enemyObject;
    public ENEMY_TYPE enemyType;
    public Navigation startingPoint;
    public List<Navigation> navPoints;


    public float speed;
    public float attackSpeed;
    public bool canReverse = true;
    public bool fullRound = false;
    public bool canEnemyStart = true;
}

public class MapPlugin : MonoBehaviour {
    private static MapPlugin _instance;

    Vector2 BlockPos;
    bool SpawnPlayer = false;
    int curX = -1;
    int curY = 0;

    public float firstBlockx = -9f;
    public float firstBlocky = 0f;
    public float xInc = 0.64f;
    public float yInc = 0.32f; // Note for Horizontal you need to - yInc

    public float PropInc = 0.46f;

    public int PlayerSpawnX;
    public int PlayerSpawnY;

    public Transform PropHolder;
    //public GameObject eGameObject;


    public List<GameObject> tileType;
    public List<GameObject> AvailableTiles;
    public List<GameObject> SortingGroup;

    public List<int> LayerAmount;

    public GameObject Player;
    public GameObject SG;
    public Transform inactive;

    public Row[] rows;
    public Prop[] props;

    public List<EnemyAI> enemyList;

    public static MapPlugin Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }


    // Update is called once per frame
    private void Start() {
        SpawnSortingGroup();
        SpawnMap();
        CheckSortAmount();
        SortNestedLayer();
        SpawnProps();
        SpawnPlayerFunc();
        SpawnEnemy();
    }

    int CheckAreas() {
        int tempBig = 1;
        for (int i = 0; i < rows.Length; i++) {
            for (int j = 0; j < rows[i].area.Length; j++) {
                if (rows[i].area[j] > tempBig) {
                    tempBig = rows[i].area[j];
                }
            }
        }
        return tempBig;
    }

    void CheckSortAmount() {
        for (int i = 0; i < rows.Length; i++) {
            int tempInt = 0;

            for (int j = 0; j < rows[i].typeBlock.Length; j++) {
                if (rows[i].typeBlock[j] != 0)
                    tempInt += rows[i].amount[j];
            }

            if (tempInt != 0) {
                LayerAmount.Add(tempInt);
            }
        }
    }

    void SortLayer() {
        int tInt = 0;
        for (int i = 0; i < LayerAmount.Count; i++) {
            tInt += LayerAmount[i];
        }
        Debug.Log(tInt);
    }

    void SpawnSortingGroup() {
        if (CheckAreas() > 1) {
            for (int i = 0; i < CheckAreas(); i++) {
                GameObject tInt = Instantiate(SG, transform);
                SortingGroup.Add(tInt);
            }
        }
    }

    void SortGroupLayer() {
        for (int i = 0; i < SortingGroup.Count; i++) {
            SortingGroup[i].GetComponent<Renderer>().sortingOrder = i;
        }
    }

    void SortNestedLayer() {
        int SortedTiles = 0;
        int rowCounter = 0;
        for (int i = 0; i < LayerAmount.Count; i++) {
            for (int j = 0; j < LayerAmount[i]; j++) {
                AvailableTiles[SortedTiles].GetComponent<SpriteRenderer>().sortingOrder = -j + rowCounter;
                SortedTiles++;
            }
            rowCounter++;
        }
    }

    void SpawnMap() {
        int counter = 0;
        for (int i = 0; i < rows.Length; i++) {
            //Iterate Through the Rows
            rows[i].Name = "Row " + (i + 1);
            SpawnVBlocks(rows[i].typeBlock[0], i + 1, rows[i].area[0]);
            for (int j = 0; j < rows[i].amount.Length; j++) {
                for (int k = 0; k < rows[i].amount[j]; k++) {
                    SpawnHBlocks(rows[i].typeBlock[j], counter + 1, rows[i].area[j]);
                    counter++;
                }
            }
            counter = 0;
        }
    }

    void SpawnProps() {
        if (props.Length > 0) {
            for (int i = 0; i < props.Length; i++) {
                if (props[i].itemData.Length > 0) {
                    for (int j = 0; j < props[i].itemData.Length; j++) {
                        GameObject tempGO = FindBlock(props[i].itemData[j].x, props[i].itemData[j].y);
                        if (tempGO != null) {
                            GameObject tempItem = null; // = eGameObject;
                            if (props[i].itemData[j].IsEmpty == false && props[i].itemData[j].item == null) {
                                tempItem = Instantiate(props[i].item, new Vector3(tempGO.transform.position.x, tempGO.transform.position.y + PropInc), transform.rotation, PropHolder);
                                props[i].itemData[j].item = tempItem;
                                tempItem.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerSort";
                            } else {
                                tempItem = null;
                            }

                            if (props[i].customOrderInLayer == true) {
                                if (props[i].itemData[j].customOrderInLayer == true) {
                                    if (tempItem != null) {
                                        tempItem.GetComponent<SpriteRenderer>().sortingOrder = props[i].itemData[j].orderInLayer;
                                    }
                                } else if (props[i].itemData[j].customOrderInLayer == false) {
                                    if (tempItem != null) {
                                        tempItem.GetComponent<SpriteRenderer>().sortingOrder = props[i].orderInLayer;
                                    }
                                }
                            } else if (props[i].customOrderInLayer == false) {
                                if (props[i].itemData[j].customOrderInLayer == true) {
                                    if (tempItem != null) {
                                        tempItem.GetComponent<SpriteRenderer>().sortingOrder = props[i].itemData[j].orderInLayer;
                                    }
                                } else if (props[i].itemData[j].customOrderInLayer == false) {
                                    if (tempItem != null) {
                                        tempItem.GetComponent<SpriteRenderer>().sortingOrder = tempGO.GetComponent<SpriteRenderer>().sortingOrder;
                                    }
                                }
                            }

                            if (tempItem != null) {
                                tempItem.GetComponent<Node>().SetXYA(props[i].itemData[j].x, props[i].itemData[j].y, tempGO.GetComponent<Node>().area);
                            }

                            if (props[i].customLayer == true) {
                                if (props[i].Layer == LAYER_TYPE.MOVABLE) {
                                    tempGO.gameObject.layer = LayerMask.NameToLayer("Movable");
                                    if (tempItem != null) {
                                        tempItem.gameObject.layer = LayerMask.NameToLayer("Movable");
                                    }
                                } else if (props[i].Layer == LAYER_TYPE.PROPS) {
                                    tempGO.gameObject.layer = LayerMask.NameToLayer("Props");
                                    if (tempItem != null) {
                                        tempItem.gameObject.layer = LayerMask.NameToLayer("Props");
                                    }
                                } else if (props[i].Layer == LAYER_TYPE.INACTIVE) {
                                    tempGO.gameObject.layer = LayerMask.NameToLayer("Inactive");
                                    if (tempItem != null) {
                                        tempItem.gameObject.layer = LayerMask.NameToLayer("Inactive");
                                    }
                                } else if (props[i].Layer == LAYER_TYPE.WALL) {
                                    tempGO.gameObject.layer = LayerMask.NameToLayer("Wall");
                                    if (tempItem != null) {
                                        tempItem.gameObject.layer = LayerMask.NameToLayer("Wall");
                                    }
                                } else if (props[i].Layer == LAYER_TYPE.INTERACT) {
                                    tempGO.gameObject.layer = LayerMask.NameToLayer("Interactive");
                                    tempGO.transform.position = new Vector3(tempGO.transform.position.x, tempGO.transform.position.y, -1);
                                    if (tempItem != null) {
                                        tempItem.gameObject.layer = LayerMask.NameToLayer("Interactive");
                                        tempItem.transform.position = new Vector3(tempItem.transform.position.x, tempItem.transform.position.y, -1);
                                    }
                                } else if (props[i].Layer == LAYER_TYPE.HACKABLE) {
                                    tempGO.gameObject.layer = LayerMask.NameToLayer("Hackable");
                                    tempGO.transform.position = new Vector3(tempGO.transform.position.x, tempGO.transform.position.y, -1);
                                    if (tempItem != null) {
                                        tempItem.gameObject.layer = LayerMask.NameToLayer("Hackable");
                                        tempItem.transform.position = new Vector3(tempItem.transform.position.x, tempItem.transform.position.y, -1);
                                    }
                                }
                            } else {
                                if (props[i].itemData[j].item != null) {
                                    if (tempItem.gameObject.layer == LayerMask.NameToLayer("Inactive")) {
                                        tempGO.gameObject.layer = LayerMask.NameToLayer("Inactive");
                                        if (tempItem != null) {
                                            tempItem.gameObject.layer = LayerMask.NameToLayer("Inactive");
                                        }
                                        //tempGO.GetComponent<SpriteRenderer>().color = Color.black;
                                    } else if (tempItem.gameObject.layer == LayerMask.NameToLayer("Props")) {
                                        tempGO.gameObject.layer = LayerMask.NameToLayer("Props");
                                        if (tempItem != null) {
                                            tempItem.gameObject.layer = LayerMask.NameToLayer("Props");
                                        }
                                    } else if (tempItem.gameObject.layer == LayerMask.NameToLayer("Wall")) {
                                        tempGO.gameObject.layer = LayerMask.NameToLayer("Wall");
                                        tempGO.GetComponent<SpriteRenderer>().color = Color.black;
                                        if (tempItem != null) {
                                            tempItem.gameObject.layer = LayerMask.NameToLayer("Wall");
                                        }
                                    } else if (tempItem.gameObject.layer == LayerMask.NameToLayer("Interact")) {
                                        tempGO.gameObject.layer = LayerMask.NameToLayer("Interact");
                                        if (tempItem != null) {
                                            tempItem.gameObject.layer = LayerMask.NameToLayer("Interact");
                                        }
                                    } else if (tempItem.gameObject.layer == LayerMask.NameToLayer("Hackable")) {
                                        tempGO.gameObject.layer = LayerMask.NameToLayer("Hackable");
                                        if (tempItem != null) {
                                            tempItem.gameObject.layer = LayerMask.NameToLayer("Hackable");
                                        }
                                    }
                                    else
                                    {
                                        tempGO.gameObject.layer = LayerMask.NameToLayer("Inactive");
                                        tempGO.GetComponent<SpriteRenderer>().color = Color.black;
                                        if (tempItem != null) {
                                            tempItem.gameObject.layer = LayerMask.NameToLayer("Inactive");
                                        }
                                    }
                                }
                            }
                            if (tempItem != null) {
                                if (tempItem.GetComponent<PropAction>() != null) {
                                    if (props[i].itemData[j].customState == true) {
                                        tempItem.GetComponent<PropAction>().MinigameType = props[i].itemData[j].State;
                                        tempItem.GetComponent<PropAction>().curZone = new ExecuteZone(props[i].itemData[j].x, props[i].itemData[j].y);
                                    } else {
                                        tempItem.GetComponent<PropAction>().MinigameType = props[i].State;
                                        tempItem.GetComponent<PropAction>().curZone = new ExecuteZone(props[i].itemData[j].x, props[i].itemData[j].y);
                                    }
                                }

                                if (props[i].IsTransparent == true) {
                                    Color tmp = tempItem.GetComponent<SpriteRenderer>().color;
                                    tmp.a = 0.8f;
                                    tempItem.GetComponent<SpriteRenderer>().color = tmp;
                                }
                            }

                            if (props[i].State == PropAction.MINIGAMETYPE.ACTIVATE_DOOR || props[i].State == PropAction.MINIGAMETYPE.ACTIVATE_DOOR_KEYCARD) {
                                tempGO.layer = LayerMask.NameToLayer("Inactive");
                            }

                        }
                    }
                }
            }
        }
    }


    void SpawnVBlocks(int BlockTypes, int hCounter, int tArea) {
        GameObject sp;
        curY = -1;
        curX++;
        sp = Instantiate(tileType[BlockTypes], new Vector3(firstBlockx + xInc * hCounter, firstBlocky - yInc * hCounter), transform.rotation, transform);
        BlockPos = sp.transform.position;
        sp.transform.SetParent(inactive);
        sp.SetActive(false);
    }

    void SpawnHBlocks(int BlockTypes, int vCounter, int tArea) {
        GameObject go;
        curY++;
        go = Instantiate(tileType[BlockTypes], new Vector3(BlockPos.x + xInc * vCounter, BlockPos.y + yInc * vCounter), transform.rotation, transform);

        if (BlockTypes == 0) {
            go.transform.SetParent(inactive);
            go.SetActive(false);
        } else {
            AvailableTiles.Add(go);
            go.GetComponent<Node>().area = tArea;
            go.GetComponent<Node>().x = curX;
            go.GetComponent<Node>().y = curY;

            if (tArea > 0) {
                go.transform.SetParent(SortingGroup[tArea - 1].transform);
            }
        }
    }

    void SpawnPlayerFunc() {
        Player = GameObject.Find("Player");
        if (SpawnPlayer == false) {
            Player.transform.position = new Vector2(FindBlock(PlayerSpawnX, PlayerSpawnY).transform.position.x, FindBlock(PlayerSpawnX, PlayerSpawnY).transform.position.y);
            Player.GetComponent<Movement>().curX = PlayerSpawnX;
            Player.GetComponent<Movement>().curY = PlayerSpawnY;
            Player.GetComponent<Movement>().OnCurTileUpdate();
            SpawnPlayer = true;
        }

        /*
		if (Player == null) {
			Player = GameObject.Find ("Player");
		} else {
			if (Player.activeInHierarchy == false) {
				Player.SetActive (true);
			}

			if (Player.activeInHierarchy == false && Player != null && SpawnPlayer == false) {
				Player.transform.position = new Vector2 (FindBlock (PlayerSpawnX, PlayerSpawnY).transform.position.x, FindBlock (PlayerSpawnX, PlayerSpawnY).transform.position.y);
				Player.GetComponent<Movement> ().curX = PlayerSpawnX;
				Player.GetComponent<Movement> ().curY = PlayerSpawnY;
				Player.GetComponent<Movement> ().OnCurTileUpdate ();
				SpawnPlayer = true;
			}
		}
		*/
    }

    void SpawnEnemy() {
        if (enemyList.Count > 0) {
            for (int i = 0; i < enemyList.Count; i++) {
                GameObject tempEnemyObject = FindBlock(enemyList[i].startingPoint.x, enemyList[i].startingPoint.y);
                Vector3 tempEnemyPosition = new Vector3(tempEnemyObject.transform.position.x, tempEnemyObject.transform.position.y); //+ 0.46f
                GameObject tempEnemy = Instantiate(enemyList[i].enemyObject, tempEnemyPosition, transform.rotation, transform);

                tempEnemyObject.layer = LayerMask.NameToLayer("Enemy");
                tempEnemy.GetComponent<SpriteRenderer>().sortingOrder = tempEnemyObject.GetComponent<SpriteRenderer>().sortingOrder;
                tempEnemy.GetComponent<SpriteRenderer>().sortingLayerName = "PlayerSort";

                GameObject temporaryNavPoint;

                if (enemyList[i].navPoints.Count > 0) {
                    for (int j = 0; j < enemyList[i].navPoints.Count; j++) {
                        if (j == enemyList[i].navPoints.Count - 1) {
                            if (enemyList[i].fullRound == false) {
                                temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y);
								//Transform tempTransform = new Vector3 ();
                                tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, enemyList[i].navPoints[j].stopTiming));
                            } else if (enemyList[i].fullRound == true) {
                                if (enemyList[i].navPoints[j].x == enemyList[i].navPoints[0].x) {
                                    temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y);
                                    tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, enemyList[i].navPoints[j].stopTiming));
                                    if (enemyList[i].navPoints[j].y > enemyList[i].navPoints[0].y) {
                                        int length = enemyList[i].navPoints[j].y - enemyList[i].navPoints[0].y;
                                        for (int k = 0; k < length + 1; k++) {
                                            temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y - k);
                                            tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                        }
                                    } else if (enemyList[i].navPoints[j].y < enemyList[i].navPoints[0].y) {
                                        int length = enemyList[i].navPoints[0].y - enemyList[i].navPoints[j].y;
                                        for (int k = 0; k < length + 1; k++) {
                                            temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y + k);
                                            tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                        }
                                    }
                                } else if (enemyList[i].navPoints[j].y == enemyList[i].navPoints[0].y) {
                                    temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y);
                                    tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, enemyList[i].navPoints[j].stopTiming));
                                    if (enemyList[i].navPoints[j].x > enemyList[i].navPoints[0].x) {
                                        int length = enemyList[i].navPoints[j].x - enemyList[i].navPoints[0].x;
                                        for (int k = 0; k < length + 1; k++) {
                                            temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x - k, enemyList[i].navPoints[j].y);
                                            tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                        }
                                    } else if (enemyList[i].navPoints[j].x < enemyList[i].navPoints[0].x) {
                                        int length = enemyList[i].navPoints[0].x - enemyList[i].navPoints[j].x;
                                        for (int k = 0; k < length + 1; k++) {
                                            temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x + k, enemyList[i].navPoints[j].y);
                                            tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                        }
                                    }
                                }
                            }
                        } else if (j < enemyList[i].navPoints.Count - 1) {
                            if (enemyList[i].navPoints[j].x == enemyList[i].navPoints[j + 1].x) {
                                temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y);
                                tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, enemyList[i].navPoints[j].stopTiming));
                                if (enemyList[i].navPoints[j].y > enemyList[i].navPoints[j + 1].y) {
                                    int length = enemyList[i].navPoints[j].y - enemyList[i].navPoints[j + 1].y;
                                    for (int k = 0; k < length + 1; k++) {
                                        temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y - k);
                                        tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                    }
                                } else if (enemyList[i].navPoints[j].y < enemyList[i].navPoints[j + 1].y) {
                                    int length = enemyList[i].navPoints[j + 1].y - enemyList[i].navPoints[j].y;
                                    for (int k = 0; k < length + 1; k++) {
                                        temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y + k);
                                        tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                    }
                                }
                            } else if (enemyList[i].navPoints[j].y == enemyList[i].navPoints[j + 1].y) {
                                temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x, enemyList[i].navPoints[j].y);
                                tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, enemyList[i].navPoints[j].stopTiming));
                                if (enemyList[i].navPoints[j].x > enemyList[i].navPoints[j + 1].x) {
                                    int length = enemyList[i].navPoints[j].x - enemyList[i].navPoints[j + 1].x;
                                    for (int k = 0; k < length + 1; k++) {
                                        temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x - k, enemyList[i].navPoints[j].y);
                                        tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                    }
                                } else if (enemyList[i].navPoints[j].x < enemyList[i].navPoints[j + 1].x) {
                                    int length = enemyList[i].navPoints[j + 1].x - enemyList[i].navPoints[j].x;
                                    for (int k = 0; k < length + 1; k++) {
                                        temporaryNavPoint = FindBlock(enemyList[i].navPoints[j].x + k, enemyList[i].navPoints[j].y);
                                        tempEnemy.GetComponent<EnemyAIPatrol>().navPointList.Add(new NavPoint(temporaryNavPoint.transform, 0));
                                    }
                                }
                            }
                        }
                    }
                }


                if (tempEnemy.GetComponent<EnemyDetectionTile>() != null) {
                    tempEnemy.GetComponent<EnemyDetectionTile>().enemyType = enemyList[i].enemyType;
                }
                tempEnemy.GetComponent<EnemyAIPatrol>().curX = enemyList[i].startingPoint.x;
                tempEnemy.GetComponent<EnemyAIPatrol>().curY = enemyList[i].startingPoint.y;

                tempEnemy.GetComponent<EnemyAIPatrol>().speed = enemyList[i].speed;
                tempEnemy.GetComponent<EnemyAIPatrol>().attackSpeed = enemyList[i].attackSpeed;

                tempEnemy.GetComponent<EnemyAIPatrol>().onCurTile = tempEnemyObject;

                tempEnemy.GetComponent<EnemyAIPatrol>().canReverse = enemyList[i].canReverse;
                tempEnemy.GetComponent<EnemyAIPatrol>().fullRound = enemyList[i].fullRound;

                tempEnemy.GetComponent<EnemyAIPatrol>().canEnemyStart = enemyList[i].canEnemyStart;
                tempEnemy.GetComponent<EnemyAIPatrol>().enemyReachedIt = true;

                if (tempEnemy.GetComponent<Node>() != null) {
                    tempEnemy.GetComponent<Node>().x = enemyList[i].startingPoint.x;
                    tempEnemy.GetComponent<Node>().y = enemyList[i].startingPoint.y;
                }
            }
        }
    }

    public GameObject FindBlock(int tempX, int tempY) {
        foreach (GameObject tm in AvailableTiles) {
            if (tempX == tm.GetComponent<Node>().x && tempY == tm.GetComponent<Node>().y) {
                return tm;
            }
        }
        return null;
    }

    public GameObject FindBlock(Collider2D tempCol) {
        foreach (GameObject tm in AvailableTiles) {
            if (tempCol.GetComponent<Node>().x == tm.GetComponent<Node>().x && tempCol.GetComponent<Node>().y == tm.GetComponent<Node>().y) {
                return tm;
            }
        }
        return null;
    }
}
