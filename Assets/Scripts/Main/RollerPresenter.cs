using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RollerView))]
public class RollerPresenter : MonoBehaviour
{
    [SerializeField] private float _sensivity;

    private PlayerInput _inputActions;
    private RollerView _rollerView;

    public event Action DeltaXChangedEvent;

    public bool IsFinished { get; private set; }

    public float DeltaX { get; private set; }

    private void Awake()
    {
        _inputActions = new PlayerInput();
        _rollerView = GetComponent<RollerView>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Rollers.Skate.performed += ctx => GetDeltaX();
    }

    private void OnDisable()
    {
        _inputActions.Rollers.Skate.performed -= ctx => GetDeltaX();
        _inputActions.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Springboard springboard))
            _rollerView.Jump();

        if (other.TryGetComponent(out Finish finish))
        {
            IsFinished = true;
            _rollerView.ChangeRollersColor();
            _rollerView.Dance();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out FinishPlane finishPlane))
            _rollerView.ChangeTrailColor();
    }

    public void GetDeltaX()
    {
        DeltaX = Mouse.current.delta.x.ReadValue() * _sensivity;
        DeltaXChangedEvent?.Invoke();
    }
}
