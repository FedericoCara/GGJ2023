using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinAgujeroEvent : MonoBehaviour
{
    [SerializeField] private RabbitController rabbitController;

    public void FinAgujero()
    {
        rabbitController.Respawn();
    }
}
