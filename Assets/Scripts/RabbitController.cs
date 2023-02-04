using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    private RabbitSpawnController _spawnController;
    private Transform _player;
    private StarterAssetsInputs _input;

    private void Start()
    {
        _spawnController = FindObjectOfType<RabbitSpawnController>();
        _player = GameObject.FindWithTag("Player").transform;
        _input = _player.GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.respawn)
        {
            Respawn(_player);
        }
    }

    public void Respawn(Transform playerPosition)
    {
        transform.position = _spawnController.GetFarthestPoint(playerPosition).position;
    }
}