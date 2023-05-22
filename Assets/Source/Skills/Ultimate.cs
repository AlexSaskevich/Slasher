using Source.Constants;
using Source.InputSource;
using Source.Interfaces;
using Source.Player;
using Source.Turntables;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerMana), typeof(InputSwitcher),typeof(UltimateTurntable))]
    public abstract class Ultimate : Skill
    {
        private PlayerMana _playerMana;
        private InputSwitcher _inputSwitcher;
        private UltimateTurntable _ultimateTurntable;
        
        [field: SerializeField] protected ParticleSystem Effect { get; private set; }
        
        public override bool CanUsed => _playerMana.CurrentValue >= Cost;
        public bool IsActive { get; protected set; }
        protected IInputSource InputSource { get; private set; }

        protected override void Awake()
        {
            _ultimateTurntable = GetComponent<UltimateTurntable>();
            base.Awake();
            _playerMana = GetComponent<PlayerMana>();
            _inputSwitcher = GetComponent<InputSwitcher>();
        }

        protected override void Start()
        {
            base.Start();
            InputSource = _inputSwitcher.InputSource;
        }

        public override void TryActivate()
        {
            _playerMana.DecreaseMana(Cost);
            _ultimateTurntable.PlayBoostSound();
            StartTimer();
        }

        protected bool CheckCurrentAnimationEnd()
        {
            var currentAnimationName = Animator.GetCurrentAnimatorStateInfo(AnimationConstants.BaseLayer)
                .IsName(AnimationConstants.Ultimate);
            var isCurrentAnimationEnd =
                Animator.GetCurrentAnimatorStateInfo(AnimationConstants.BaseLayer).normalizedTime >=
                AnimationConstants.EndAnimationTime;

            return currentAnimationName && isCurrentAnimationEnd;
        }
    }
}