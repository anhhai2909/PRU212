using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 5f;
    public int numSegments = 100;

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSegments + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
