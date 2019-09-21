using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LevelPath : MonoBehaviour
{
    public PathTile [] path;

    struct Waypoint
    {
        public Transform transform;
        public float distanceFromStart;
    }

    public float TotalDistance
    {
        get;
        private set;
    }
    
    private List<Waypoint> wayPoints;

    void Awake()
    {
        CreateWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetWorldPosition(float distanceToStart)
    {
        int wayPointIndex = GetWaypointIndex(distanceToStart);

        if (wayPointIndex == wayPoints.Count - 1)
        {
            return wayPoints[wayPoints.Count - 1].transform.position;
        }
        
        float t = (distanceToStart - wayPoints[wayPointIndex].distanceFromStart) / (wayPoints[wayPointIndex + 1].distanceFromStart - wayPoints[wayPointIndex].distanceFromStart);
        return Vector3.Lerp(wayPoints[wayPointIndex].transform.position,
            wayPoints[wayPointIndex + 1].transform.position, t);
    } 


    private int GetWaypointIndex(float distanceToStart)
    {
        for (int i = 0; i < wayPoints.Count; ++i)
        {
            if (distanceToStart <= wayPoints[i].distanceFromStart)
            {
                return Mathf.Max(0, i - 1);
            }
        }

        return wayPoints.Count - 1;
    }

    private void CreateWaypoints()
    {
        wayPoints = new List<Waypoint>();
        float accumulatedDistance = 0;
        Transform previousPoint = null;
        
        foreach(PathTile tile in path)
        {
            foreach (Transform point in tile.points)
            {
                float distanceToPrevious = 0;
                if (previousPoint != null)
                {
                    distanceToPrevious = (point.position - previousPoint.position).magnitude;
                }
                
                accumulatedDistance += distanceToPrevious;
                
                wayPoints.Add(new Waypoint
                {
                    transform = point,
                    distanceFromStart = accumulatedDistance
                });

                
                previousPoint = point;
            }
        }

        TotalDistance = accumulatedDistance;
    }
}
