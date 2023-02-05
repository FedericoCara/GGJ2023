using System;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawnController : MonoBehaviour
{
    private List<SpawnPoint> spawnPoints;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());
    }

    public SpawnPoint GetSpawnPoint(Transform playerTransform)
    {
        float maxSqrDistance = 0, currentDistance;
        SpawnPoint farthestPoint = null;
        foreach (var spawnPoint in spawnPoints)
        {
            if ((currentDistance=Vector3.SqrMagnitude(playerTransform.position - spawnPoint.transform.position)) > maxSqrDistance)
            {
                maxSqrDistance = currentDistance;
                farthestPoint = spawnPoint;
            }
        }

        return farthestPoint;
    }
}