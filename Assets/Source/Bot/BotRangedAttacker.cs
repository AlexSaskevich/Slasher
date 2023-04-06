using Source.Infrastructure;
using UnityEngine;

namespace Source.Bot
{
    public sealed class BotRangedAttacker : BotAttacker
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _bulletSpawnPoint;
        
        public override void Attack()
        {
            var newBullet = Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.identity, null);
            newBullet.Init(PlayerHealth.transform);
        }
    }
}