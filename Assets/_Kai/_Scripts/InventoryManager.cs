using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryList;

public class InventoryManager : MonoBehaviour {
    private static InventoryManager _instance;

    public List<InventoryList.Inventory> playerItems;
    public List<Sprite> MasterSprite;

    public static InventoryManager Instance {
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

    private void Update() {
        if(playerItems.Count != 0) {
            for(int i = 0; i < playerItems.Count; i++) {
                if(playerItems[i].item == Inventory.IITEM.KEYCARD) {
                    playerItems[i].spr = MasterSprite[0];
                }
            }
        }
    }

    public int SearchItem(Inventory.IITEM Name) {
        for(int i = playerItems.Count - 1; i > -1; i--) {
            if (playerItems[i].item == Name) {
                return i;
            }
        }
        return -1;
    }
}
