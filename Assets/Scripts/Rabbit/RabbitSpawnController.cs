using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RabbitSpawnController : MonoBehaviour
{
    [SerializeField]
    private float minDistanceSqr = 100;

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
        var distantPoints = spawnPoints.FindAll(point =>
            Vector3.SqrMagnitude(point.transform.position - playerTransform.position) >= minDistanceSqr);

        return distantPoints[Random.Range(0,distantPoints.Count)];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(minDistanceSqr));
    }
}