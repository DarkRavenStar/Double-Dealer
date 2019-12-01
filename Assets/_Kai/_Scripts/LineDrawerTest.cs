using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawerTest : MonoBehaviour {
    private PolygonCollider2D pColider;

    void Start() {
        pColider = this.GetComponent<PolygonCollider2D>();
        highlightAroundCollider(pColider, Color.white, Color.white, 0.1f);
    }


    void highlightAroundCollider(Component cpType, Color beginColor, Color endColor, float hightlightSize = 0.3f) {
        //1. Create new Line Renderer
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null) {
            lineRenderer = cpType.gameObject.AddComponent<LineRenderer>();

        }

        //2. Assign Material to the new Line Renderer
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));

        float zPos = 10f;//Since this is 2D. Make sure it is in the front

        if (cpType is PolygonCollider2D) {
            //3. Get the points from the PolygonCollider2D
            Vector2[] pColiderPos = (cpType as PolygonCollider2D).points;

            //Set color and width
            lineRenderer.startColor = beginColor;
            lineRenderer.endColor = beginColor;
            lineRenderer.startWidth = hightlightSize;
            lineRenderer.endWidth = hightlightSize;

            //4. Convert local to world points
            for (int i = 0; i < pColiderPos.Length; i++) {
                pColiderPos[i] = cpType.transform.TransformPoint(pColiderPos[i]);
            }

            //5. Set the SetVertexCount of the LineRenderer to the Length of the points
            lineRenderer.positionCount = pColiderPos.Length + 1;
            for (int i = 0; i < pColiderPos.Length; i++) {
                //6. Draw the  line
                Vector3 finalLine = pColiderPos[i];
                finalLine.z = zPos;
                lineRenderer.SetPosition(i, finalLine);

                //7. Check if this is the last loop. Now Close the Line drawn
                if (i == (pColiderPos.Length - 1)) {
                    finalLine = pColiderPos[0];
                    finalLine.z = zPos;
                    lineRenderer.SetPosition(pColiderPos.Length, finalLine);
                    lineRenderer.sortingOrder = 100;
                }
            }
        }
    }
}