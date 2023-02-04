using System;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawnController : MonoBehaviour
{
    private List<Transform> spawnPoints;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
        spawnPoints.Remove(transform);
    }

    public Transform GetFarthestPoint(Transform playerTransform)
    {
        float maxSqrDistance = 0, currentDistance;
        Transform farthestPoint = null;
        foreach (var spawnPoint in spawnPoints)
        {
            if ((currentDistance=Vector3.SqrMagnitude(playerTransform.position - spawnPoint.position)) > maxSqrDistance)
            {
                maxSqrDistance = currentDistance;
                farthestPoint = spawnPoint;
            }
        }

        return farthestPoint;
    }
}