using System;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        if (_particleSystem == null)
            throw new NullReferenceException(nameof(_particleSystem));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RollerGirl rollerGirl))
            _particleSystem.Play();
    }
}
