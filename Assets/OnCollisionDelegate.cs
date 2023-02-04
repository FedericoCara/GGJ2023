using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDelegate : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        target.SendMessage("OnTriggerEnter",other);
    }
}
