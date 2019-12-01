using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public GameObject sound;

    public int inventorySize;
    public int widthPadding;

    public float IImageWidth;

    public GameObject InventoryObject;

    public List<GameObject> InventoryList;


    private void Start()
    {
        SpawnInventory();
    }

    void SpawnInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            float nextImage = -((widthPadding + IImageWidth) * (i + 1));
            GameObject tempUI = Instantiate(InventoryObject, this.transform);
            tempUI.GetComponent<InventoryItem>().InvID = i;

            RectTransform tempInvRT = tempUI.GetComponent<RectTransform>();
            //Debug.Log(nextImage);
            tempInvRT.localPosition = new Vector3(nextImage, -10);

            InventoryList.Add(tempUI);
        }
    }

    public void OnMouseDown()
    {
        SoundManager.Instance.OpenMenuSource.Play();
    }
    public void MouseDown()
    {
        SoundManager.Instance.ClickSource.Play();
    }
    public void MouseDown1()
    {
        SoundManager.Instance.CloseMenuSource.Play();
    }
}
