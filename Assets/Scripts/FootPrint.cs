using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootPrint : MonoBehaviour
{
    [SerializeField]
    private List<float> fillRates;

    [SerializeField] private float stepTime = 0.5f;
    [SerializeField] private AudioSource footPrintAudio;
    [SerializeField] private List<AudioClip> pasitos;

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
            if(i%3==0)
                footPrintAudio.PlayOneShot(pasitos[Random.Range(0,pasitos.Count)]);
            yield return new WaitForSeconds(stepTime);
        }
    }
}
