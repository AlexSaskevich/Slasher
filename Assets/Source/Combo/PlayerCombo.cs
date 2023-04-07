﻿using UnityEngine;
using UnityEngine.Events;

namespace Source.Combo
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerCombo : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private readonly IdleState _idleState = new();
        private Animator _animator;

        public event UnityAction Attacked;
        public event UnityAction StateChanged;

        public State CurrentState { get; private set; }
        public Animator Animator { get { return _animator; } }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            CurrentState = _idleState;
            CurrentState.Enter(this);
        }

        private void Update()
        {
            CurrentState.Update(this);
        }

        public void SwitchState(State newState)
        {
            if (newState == null)
                return;

            CurrentState = newState;
            newState.Enter(this);
            StateChanged?.Invoke();
        }

        public void StartDetectCollisions()
        {
            _weapon.StartDetectCollisions();
            Attacked?.Invoke();
        }

        public void StopDetectCollisions()
        {
            _weapon.StopDetectCollisions();
        }
    }
}