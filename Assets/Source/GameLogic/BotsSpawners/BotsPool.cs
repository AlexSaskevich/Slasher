using System.Collections.Generic;
using System.Linq;
using Source.Bot;
using UnityEngine;

namespace Source.GameLogic.BotsSpawners
{
    public abstract class BotsPool : MonoBehaviour
    {
        private readonly List<BotMovement> _pool = new();

        protected void Init(BotMovement botMovement, Vector3 spawnPoint)
        {
            var newBot = Instantiate(botMovement, spawnPoint, Quaternion.identity, transform);
            newBot.gameObject.SetActive(false);
            
            _pool.Add(newBot);
        }

        protected bool TryGetBot(out BotMovement botMovement)
        {
            botMovement = _pool.FirstOrDefault(bot => bot.gameObject.activeSelf == false);

            return botMovement != null;
        }
    }
}