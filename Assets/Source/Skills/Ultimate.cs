using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    public abstract class Ultimate : Skill
    {
        public PlayerMana PlayerMana { get; protected set; }
        public Animator Animator { get; protected set; }

        public override void TryActivate()
        {
            PlayerMana.DecreaseMana(Cost);
            StartTimer();
        }
    }
}