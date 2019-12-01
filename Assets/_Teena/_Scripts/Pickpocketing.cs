using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryList;

public class Pickpocketing : MonoBehaviour {

    Enemy enemy;
    RaycastHit2D hit;

    public InventoryItem inv;
    public GameObject sound;
	public bool canPlayerPickpocket = false;
    private void Start()
    {
        sound = GameObject.Find("SoundManager");
    }
    private void Update() {
		if (canPlayerPickpocket == true) {
			if(ScreenMouseRay() != null && Input.GetMouseButtonDown(0)) {
				Collider2D[] tempCol = ScreenMouseRay();
				for(int i = 0; i < tempCol.Length; i++) {
					if (tempCol [i].transform.CompareTag ("Enemy")) {
						enemy = tempCol [i].GetComponent<Enemy> ();
						if (enemy.canBePickpocketed == true) {
							if (enemy.GetComponent<PropAction> () == null) {
								Pickpocket ();
							}
                            SoundManager.Instance.PickPocketSource.Play();
						}
					} else {
						canPlayerPickpocket = false;
					}
				}
			}
			canPlayerPickpocket = false;
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

    //Pickpocket function
    void Pickpocket() {

        //If item did not exist in the player inventory, add in the items
        for (int i = 0; i < enemy.enemyItems.Count; i++) {
            Inventory tempEnemyItem = enemy.enemyItems[i];
            if (enemy.gameObject.GetComponent<PickpocketUI>() != null)
            {
                enemy.gameObject.GetComponent<PickpocketUI>().pickPocketed = true;
            }
            InventoryManager.Instance.playerItems.Add(tempEnemyItem);
            enemy.enemyItems.Remove(enemy.enemyItems[i]);
        }
    }
}