using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryList
{
	//Never remove  [System.Serializable] if you want the variables to show in the inspector
	[System.Serializable]
	public class Inventory
	{
        public enum IITEM {
            NONE = -1,
            KEYCARD
        }

        public Sprite spr;

        public IITEM item;

        public float numOfItems;
	}
}
