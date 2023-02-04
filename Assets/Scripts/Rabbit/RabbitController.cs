using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    [Header("Danger")]
    [SerializeField] private float minStandingDistanceSqr = 25;

    [SerializeField] private float minCrouchingDistanceSqr = 4;
    [SerializeField] private float dangerSpeedNormal = 0.1f;
    [SerializeField] private float dangerSpeedTooClose = 0.2f;
    private float MinStandingDistance => Mathf.Sqrt(minStandingDistanceSqr);
    private float MinCrouchingDistance => Mathf.Sqrt(minCrouchingDistanceSqr);
    public float AlertPercentage => _alertPercentage;

    private Animator _animator;
    private RabbitSpawnController _spawnController;
    private FirstPersonController _player;
    private Transform _playerTransform;
    private StarterAssetsInputs _input;
    private float _alertPercentage;
    private static readonly int AlertParameter = Animator.StringToHash("Alert");
    private static readonly int HideParameter = Animator.StringToHash("Hide");
    private bool _escaping = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
            Respawn();
        }
    }

    private void CheckPlayer()
    {
        if(_escaping)
            return;
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
        _escaping = true;
        _animator.SetTrigger(HideParameter);
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
        if (_alertPercentage >= 1)
        {
            Escape();
            return;
        }
        if(!_animator.GetBool(AlertParameter))
            _animator.SetBool(AlertParameter, true);
    }

    private void DecreaseDanger()
    {
        _alertPercentage = Mathf.Clamp01(_alertPercentage - Time.deltaTime * dangerSpeedNormal);
        if(_animator.GetBool(AlertParameter))
            _animator.SetBool(AlertParameter, false);
    }

    private void Respawn()
    {
        transform.position = _spawnController.GetFarthestPoint(_playerTransform).position;
        _alertPercentage = 0;
        _escaping = false;
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