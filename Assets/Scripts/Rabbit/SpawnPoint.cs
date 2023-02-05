using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}