using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    //[SerializeField] private Animator _animator;
    [Header("Danger")]
    [SerializeField] private float minStandingDistanceSqr = 25;
    [SerializeField] private float minCrouchingDistanceSqr = 4;
    [SerializeField] private float dangerSpeedNormal = 0.1f;
    [SerializeField] private float dangerSpeedTooClose = 0.2f;
    private float MinStandingDistance => Mathf.Sqrt(minStandingDistanceSqr);
    private float MinCrouchingDistance => Mathf.Sqrt(minCrouchingDistanceSqr);
    public float AlertPercentage => _alertPercentage;

    private RabbitSpawnController _spawnController;
    private FirstPersonController _player;
    private Transform _playerTransform;
    private StarterAssetsInputs _input;
    private float _alertPercentage;
    //private static readonly int Danger = Animator.StringToHash("Danger");

    private void Start()
    {
        _spawnController = FindObjectOfType<RabbitSpawnController>();
        _player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        _playerTransform = _player.transform;
        _input = _playerTransform.GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        TestRespawn();
        CheckPlayer();
    }

    private void TestRespawn()
    {
        if (_input.respawn)
        {
            Respawn(_playerTransform);
        }
    }

    private void CheckPlayer()
    {
        PlayerAlertLevel playerAlertLevel = GetPlayerAlertLevel();
        switch (playerAlertLevel)
        {
            case PlayerAlertLevel.FAR:
            case PlayerAlertLevel.CLOSE_CROUCHING:
                DecreaseDanger();
                break;
            case PlayerAlertLevel.CLOSE_STANDING:
                IncreaseDanger(dangerSpeedNormal);
                break;
            case PlayerAlertLevel.TOO_CLOSE_CROUCHING:
                IncreaseDanger(dangerSpeedTooClose);
                break;
            case PlayerAlertLevel.TOO_CLOSE_STANDING:
                Escape();
                break;
        }
    }

    private void Escape()
    {
        
        Respawn(_playerTransform);
    }

    private PlayerAlertLevel GetPlayerAlertLevel()
    {
        var sqrDistance = Vector3.SqrMagnitude(_playerTransform.position - transform.position);
        if (!_player.IsCrouched)
        {
            return sqrDistance < minCrouchingDistanceSqr ? PlayerAlertLevel.TOO_CLOSE_STANDING :
                sqrDistance < minStandingDistanceSqr ? PlayerAlertLevel.CLOSE_STANDING :
                PlayerAlertLevel.FAR;
        }
        else
        {
            return sqrDistance < minCrouchingDistanceSqr ? PlayerAlertLevel.TOO_CLOSE_CROUCHING :
                sqrDistance < minStandingDistanceSqr ? PlayerAlertLevel.CLOSE_CROUCHING :
                PlayerAlertLevel.FAR;
        }
    }

    private void IncreaseDanger(float dangerSpeed)
    {
        _alertPercentage = Mathf.Clamp01(_alertPercentage + Time.deltaTime * dangerSpeed);
        //_animator.SetInteger(Danger, 1);
    }

    private void DecreaseDanger()
    {
        _alertPercentage = Mathf.Clamp01(_alertPercentage - Time.deltaTime * dangerSpeedNormal);
        //_animator.SetInteger(Danger, 0);
    }

    private void Respawn(Transform playerPosition)
    {
        transform.position = _spawnController.GetFarthestPoint(playerPosition).position;
        _alertPercentage = 0;
    }


    private void OnDrawGizmosSelected()
    {
        var currentPosition = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentPosition, MinStandingDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(currentPosition, MinCrouchingDistance);
    }
}