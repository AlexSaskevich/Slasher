using UnityEngine;

namespace Source.Bot
{
    public sealed class BotClosedAttacker : BotAttacker
    {
        [SerializeField] private float _damage;
        
        public override void Attack()
        {
            PlayerHealth.TryTakeDamage(_damage);
        }
    }
}