using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;

    private Vector3[] points;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
        points = null;
    }

    private void OnEnable()
    {
        lr.positionCount = 0;
        points = null;
    }

    public void SetupLine(Vector3[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (points == null) return;
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }
}
