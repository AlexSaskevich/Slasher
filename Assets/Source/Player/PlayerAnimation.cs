using Source.Constants;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool isRunning = _rigidbody.velocity.normalized.magnitude > 0;
        Animate(isRunning);
    }

    private void Animate(bool value)
    {
        _animator.SetBool(AnimationConstants.IsRunning, value);
    }
}