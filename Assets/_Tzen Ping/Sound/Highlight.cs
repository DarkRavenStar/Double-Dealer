using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

    public Sprite object1;
    public Sprite object2;
    public Sprite object3;
    public Sprite object4;
    public Sprite object5;
    public Sprite object6;
    public Sprite object7;
    public Sprite object8;
    bool isOver = false;
    bool isOver1 = false;
    bool isOver2 = false;
    bool isOver3 = false;

    private void Update() {
        if (isOver == true) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object1) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object2;
            }
        } else {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object2) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object1;
            }
        }

        if (isOver1 == true) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object3) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object4;
            }
        } else {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object4) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object3;
            }
        }

        if (isOver2 == true) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object5) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object6;
            }
        } else {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object6) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object5;
            }
        }

        if (isOver3 == true) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object7) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object8;
            }
        } else {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == object8) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = object7;
            }
        }

    }
    public void OnMouseOver() {
        Debug.Log("Mouse over");
        isOver = true;
        isOver1 = true;
        isOver2 = true;
        isOver3 = true;
    }
    public void OnMouseExit() {
        Debug.Log("Mouse Exit");
        isOver = false;
        isOver1 = false;
        isOver2 = false;
        isOver3 = false;
    }
}

