using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RabbitSpawnController : MonoBehaviour
{
    [SerializeField] private FootPrint footprintPrefab;
    [SerializeField] private float minDistanceSqr = 100;

    private List<SpawnPoint> spawnPoints = new();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        spawnPoints.Clear();
        spawnPoints.AddRange(GetComponentsInChildren<SpawnPoint>());
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

    public void GenerateFootprints()
    {
        Initialize();
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.GenerateFootprints(footprintPrefab);
        }
    }
}