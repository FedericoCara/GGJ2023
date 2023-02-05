using System;
using UnityEngine;

namespace Rabbit
{
    public class SaltaTierraEvent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void SaltaTierra()
        {
            Debug.Log("Displaying Salta Tierra sound");
            _audioSource.Play();
        }
    }
}