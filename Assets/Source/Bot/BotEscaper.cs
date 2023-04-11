using UnityEngine;

namespace Source.Bot
{
    public sealed class BotEscaper : MonoBehaviour
    {
        public Transform Player { get; private set; }

        public void Init(Transform player)
        {
            Player = player;
        }
    }
}