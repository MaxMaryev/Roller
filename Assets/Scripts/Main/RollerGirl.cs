using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(RollerPresenter))]
public class RollerGirl : MonoBehaviour
{
    [SerializeField] private float _speed;
    private RollerPresenter _presenter;
    private Rigidbody _rigidbody;

    public float DistanceBetweenRollers { get; private set; }

    private void Awake()
    {
        if (_speed <= 0)
            throw new ArgumentOutOfRangeException("Некорректно задана скорость");

        _rigidbody = GetComponent<Rigidbody>();
        _presenter = GetComponent<RollerPresenter>();
    }

    private void Update()
    {
        if (_presenter.IsFinished)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        MoveForward();
    }

    private void OnEnable()
    {
        _presenter.DeltaXChangedEvent += SetDistanceBetweenRollers;
    }

    private void OnDisable()
    {
        _presenter.DeltaXChangedEvent -= SetDistanceBetweenRollers;
    }

    private void SetDistanceBetweenRollers()
    {
        DistanceBetweenRollers += _presenter.DeltaX;
        DistanceBetweenRollers = Mathf.Clamp01(DistanceBetweenRollers);
    }

    private void MoveForward()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _speed);
    }
}
