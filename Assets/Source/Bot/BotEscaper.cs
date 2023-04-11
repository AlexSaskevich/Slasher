using UnityEngine;

namespace Source.Bot
{
    public sealed class BotEscaper : MonoBehaviour
    {
        public Transform PlayerTransform { get; private set; }

        public void Init(Transform player)
        {
            PlayerTransform = player;
        }
    }
}