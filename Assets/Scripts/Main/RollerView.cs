using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RollerGirl), typeof(RollerPresenter))]
public class RollerView : MonoBehaviour
{
    [SerializeField] private Gradient _finishTrailColor;
    [SerializeField] private Material _finishRollersMaterial;
    [SerializeField] private SkinnedMeshRenderer _rollersRenderer;

    private RollerGirl _rollerGirl;
    private Animator _animator;
    private RollerPresenter _presenter;
    private TrailRenderer[] _trails;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rollerGirl = GetComponent<RollerGirl>();
        _presenter = GetComponent<RollerPresenter>();
        _trails = GetComponentsInChildren<TrailRenderer>();

        Validate();
    }

    private void Start()
    {
        StartCoroutine(IncreasingTrailLifeTime());
    }

    private void OnEnable()
    {
        _presenter.DeltaXChangedEvent += BendDown;
    }

    private void OnDisable()
    {
        _presenter.DeltaXChangedEvent -= BendDown;
    }

    public void Jump()
    {
        const string Jump = "Jump";
        _animator.Play(Jump);
    }

    public void Dance()
    {
        const string Dance = "Dance";
        _animator.Play(Dance);
    }

    public void ChangeTrailColor()
    {
        foreach (TrailRenderer trail in _trails)
        {
            trail.colorGradient = _finishTrailColor;
        }
    }

    public void ChangeRollersColor()
    {
        _rollersRenderer.material = _finishRollersMaterial;
    }

    private void BendDown()
    {
        const string BendDown = "BendDown";
        _animator.SetFloat(BendDown, _rollerGirl.DistanceBetweenRollers);
    }

    private void Validate()
    {
        const int Pair = 2;

        if (_trails.Length < Pair)
            throw new NullReferenceException("Отсутсвует компонент TrailRenderer на ролике");

        if (_finishTrailColor == null)
            throw new NullReferenceException(nameof(_finishTrailColor));

        if (_finishRollersMaterial == null)
            throw new NullReferenceException(nameof(_finishRollersMaterial));

        if (_rollersRenderer == null)
            throw new NullReferenceException(nameof(_rollersRenderer));
    }

    private IEnumerator IncreasingTrailLifeTime()
    {
        float delay = 0.1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
        float step = 0.02f;
        float targetLifeTime = 3f;

        while (_trails[default].time < targetLifeTime)
        {
            foreach (TrailRenderer trail in _trails)
            {
                trail.time = Mathf.MoveTowards(trail.time, targetLifeTime, step);
            }

            yield return waitForSeconds;
        }
    }
}
