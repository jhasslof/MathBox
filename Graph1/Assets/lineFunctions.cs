using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class lineFunctions : MonoBehaviour
{
    //Public variables can be set to game objects in the Unity Editor
    public LineRenderer lineRenderer;
    public Transform point0;
    public Transform point1;

    // Private variables are set in this script
    private float stepSize;
    private float t;
    private int posIndex = 0;
    private float updateInerval;
    private float nextTime = 0;
    private bool continueDrawing = true;
    private int numberOfFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Alt 1. Draw the line at Startup, without using the Update-function.
        //          This draw a static line
        //DrawTheLineAtStartup();
        //DrawTheLineAtStartupVer2();

        // Alt 2. Use the Update-function to give new linepositions inside the Update loop.
        //          This makes a line animation
        InitializeUpdateLinePositions();
    }

    // Update is called once per frame
    void Update()
    {
        //Alt 2
        UpdateLinePositions();
    }

    private void InitializeUpdateLinePositions()
    {
        // Here we have our game objects as input to the script in the Unity editor.
        // If you don't have the game objects as input parmeters to the script you can find your objects like this
        //
        //point0 = GameObject.Find("BluePoint").transform;
        //point1 = GameObject.Find("RedPoint").transform;
        //lineRenderer = GameObject.FindObjectsOfType<LineRenderer>().Single( r => r.name == "Line");

        t = 0.0f;
        updateInerval = 0.2f; // <-- We delay update to have time to see what happens on screen
        stepSize = (1 / (float)60);
        nextTime = 0;
        lineRenderer.positionCount = 0;
    }

    private void UpdateLinePositions()
    {
        if (Time.time >= nextTime)
        {
            Debug.Log("Update @" + Time.time);

            if (continueDrawing)
            {
                Vector3 pos = CalculateLinePoint(t, point0.position, point1.position);
                lineRenderer.positionCount = lineRenderer.positionCount + 1;
                Debug.Log(lineRenderer.positionCount);

                lineRenderer.SetPosition(posIndex, pos);
                posIndex += 1;
                t += stepSize;
                if (t > 1)
                {
                    // Render last position
                    t = 1;
                    pos = CalculateLinePoint(t, point0.position, point1.position);
                    lineRenderer.positionCount = lineRenderer.positionCount + 1;
                    Debug.Log("Last position = " + lineRenderer.positionCount);

                    lineRenderer.SetPosition(posIndex, pos);
                    continueDrawing = false;
                }
            }
            nextTime += updateInerval;
        }
    }

    private void DrawTheLineAtStartup()
    {
        Debug.Log("Run DrawTheLineAtStartup");

        //-------------------------------------------------------------
        //frame                     |01|02|03|04|05|06|07|08|09|10|11 |
        //percentMovedFromP0ToP1    |00|10|20|30|40|50|60|70|80|90|100|
        //X-positions               |-5|-4|-3|-2|-1|00|+1|+2|+3|+4|+5 |
        //indexInVector             |00|01|02|03|04|05|06|07|08|09|10 |
        //-------------------------------------------------------------
        numberOfFrames = 11;

        //Line ends
        point0.position = new Vector3(-5, 1, 0);
        point1.position = new Vector3(5, 1, 0);

        //Initialize line positions
        lineRenderer.positionCount = numberOfFrames;
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

    private void DrawTheLineAtStartupVer2()
    {
        Debug.Log("Run DrawTheLineAtStartupVer2");

        //-------------------------------------------------------------
        //frame                     |01|02|03|04|05|06|07|08|09|10|11 |
        //percentMovedFromP0ToP1    |00|10|20|30|40|50|60|70|80|90|100|
        //X-positions               |-5|-4|-3|-2|-1|00|+1|+2|+3|+4|+5 |
        //indexInVector             |00|01|02|03|04|05|06|07|08|09|10 |
        //-------------------------------------------------------------
        numberOfFrames = 11;

        //Line ends
        point0.position = new Vector3(-5, 1, 0);
        point1.position = new Vector3(5, 1, 0);

        //Initialize line positions
        lineRenderer.positionCount = numberOfFrames;
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
