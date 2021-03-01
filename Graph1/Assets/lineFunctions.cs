using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineFunctions : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform point0;
    public Transform point1;

    private static int numberOfFrames = 11;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = numberOfFrames;

        // Draw the line at Startup
        DrawLinerCurve();
    }


    // Update is called once per frame
    void Update()
    {
        // No updates done here
    }

    private void DrawLinerCurve()
    {
        Debug.Log("Run DrawLinerCurve");
        point0.position = new Vector3(-5, 1, 0);
        point1.position = new Vector3(5, 1, 0);

        //frame                     |01|02|03|04|05|06|07|08|09|10|11 |
        //percentMovedFromP0ToP1    |00|10|20|30|40|50|60|70|80|90|100|
        //X-positions               |-5|-4|-3|-2|-1|00|+1|+2|+3|+4|+5 |
        //indexInVector             |00|01|02|03|04|05|06|07|08|09|10 |
        Vector3[] positions = new Vector3[numberOfFrames];

        // For each frame, calculate position for percent moved from P0 to P1
        for (int frame = 1; frame <= numberOfFrames; frame++)
        {
            float percentMovedFromP0ToP1;
            if (frame == 1)
            {
                percentMovedFromP0ToP1 = 0;
            }
            else
            {
                percentMovedFromP0ToP1 = (float)(frame - 1) / (float)(numberOfFrames - 1);
            }
            
            int indexInVector = frame - 1;
            positions[indexInVector] = CalculateLinePoint(percentMovedFromP0ToP1, point0.position, point1.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private void DrawLinerCurve2()
    {
        Debug.Log("Run DrawLinerCurve2");
        point0.position = new Vector3(-5, 1, 0);
        point1.position = new Vector3(5, 1, 0);

        //frame                     |01|02|03|04|05|06|07|08|09|10|11 |
        //percentMovedFromP0ToP1    |00|10|20|30|40|50|60|70|80|90|100|
        //X-positions               |-5|-4|-3|-2|-1|00|+1|+2|+3|+4|+5 |
        //indexInVector             |00|01|02|03|04|05|06|07|08|09|10 |
        Vector3[] positions = new Vector3[numberOfFrames];

        // For each frame, calculate position for percent moved from P0 to P1
        for (int indexInVector = 0; indexInVector < numberOfFrames; indexInVector++)
        {
            float percentMovedFromP0ToP1 = (float)(indexInVector) / (float)(numberOfFrames - 1);
            positions[indexInVector] = CalculateLinePoint(percentMovedFromP0ToP1, point0.position, point1.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateLinePoint(float t, Vector3 p0, Vector3 p1)
    {
        // http://www.theappguruz.com/blog/bezier-curve-in-games
        //  position = P0 + t(P1 – P0)
        //  t = 0 => position = p0
        //  t = 1 => position = p1

        Vector3 position =  p0 + t * (p1 - p0);

        return position;

    }

}
