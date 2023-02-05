using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RabbitSpotSound : MonoBehaviour
{
    private AudioSource spottedAudioSource;
    private RabbitController _rabbitController;

    void Awake()
    {
        _rabbitController = FindObjectOfType<RabbitController>();
        _rabbitController.OnRabbitSpotted += OnRabbitSpotted;
        spottedAudioSource = GetComponent<AudioSource>();
    }

    private void OnRabbitSpotted()
    {
        spottedAudioSource.Play();
    }
}
