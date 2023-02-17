using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float stepTime = 2f;
    private List<FootPrint> _footPrints;

    private void Awake()
    {
        _footPrints = new List<FootPrint>(GetComponentsInChildren<FootPrint>());
    }

    public void CleanFootprints()
    {
        foreach (var footPrint in _footPrints)
        {
            footPrint.Clean();
        }
    }

    public void DrawFootprints()
    {
        StartCoroutine(DrawCoroutine());
    }
    
    private IEnumerator DrawCoroutine()
    {
        yield return new WaitForSeconds(stepTime);
        foreach (var footPrint in _footPrints)
        {
            footPrint.Draw();
            yield return new WaitForSeconds(stepTime);
        }
    }

    public void GenerateFootprints(FootPrint footprintPrefab)
    {
        Debug.Log($"Generating footprints for {name}");
        Vector3 sourcePosition = Vector3.one;

        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(sourcePosition, transform.position, NavMesh.AllAreas, path);
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 10);
            SpawnFootprint(path.corners[i], footprintPrefab);
        }
    }

    private void SpawnFootprint(Vector3 pathCorner, FootPrint footprint)
    {
        var newFootprint = Instantiate(footprint, transform);
        newFootprint.transform.position = pathCorner;
        Destroy(newFootprint.gameObject, 10);
    }
}