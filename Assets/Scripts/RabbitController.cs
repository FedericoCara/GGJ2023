using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    [SerializeField] private RabbitSpawnController _spawnController;

    public void Respawn(Transform playerPosition)
    {
        transform.position = _spawnController.GetFarthestPoint(playerPosition).position;
    }
}