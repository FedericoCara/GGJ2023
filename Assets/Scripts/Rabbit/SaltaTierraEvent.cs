using System;
using UnityEngine;

namespace Rabbit
{
    public class SaltaTierraEvent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void SaltaTierra()
        {
            _audioSource.Play();
        }
    }
}