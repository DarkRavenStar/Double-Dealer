using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

    public int InvID = 0;


    // Update is called once per frame
    void Update()
    {
        if (InvID < InventoryManager.Instance.playerItems.Count)
        {
            //if()
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = InventoryManager.Instance.playerItems[InvID].spr;
    }

    public void ClearSprite()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = null;
    }
}
