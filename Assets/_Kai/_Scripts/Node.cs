using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public int x = -1;
    public int y = -1;
    public int area = -1;

    public Node(int tx, int ty) {
        x = tx;
        y = ty;
    }

    public void SetXYA(int tx, int ty, int a) {
        x = tx;
        y = ty;
        area = a;
    }

    public void SetXYA(int tx, int ty) {
        x = tx;
        y = ty;
    }
}
