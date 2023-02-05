using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    [SerializeField]
    private List<float> fillRates;

    [SerializeField] private float stepTime = 0.5f;

    private MeshRenderer _meshRenderer;
    private Material _material;
    private static readonly int FillRate = Shader.PropertyToID("_FillRate");

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _material = Instantiate(_meshRenderer.material);
        Clean();
    }

    public void Clean()
    {
        _material.SetFloat(FillRate, 0);
    }

    public void Draw()
    {
        StartCoroutine(DrawCoroutine());
    }

    private IEnumerator DrawCoroutine()
    {
        for (int i = 0; i < fillRates.Count; i++)
        {
            _material.SetFloat(FillRate, fillRates[i]);
            yield return new WaitForSeconds(stepTime);
        }
    }
}
