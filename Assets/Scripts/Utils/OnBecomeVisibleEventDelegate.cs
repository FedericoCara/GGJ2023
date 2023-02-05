using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBecomeVisibleEventDelegate : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void OnBecameVisible()
    {
        target.SendMessage(nameof(OnBecameVisible));
    }
}
