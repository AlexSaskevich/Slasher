using Source.Enums;
using UnityEngine;

namespace Source.UI.Views
{
    public sealed class PlayerCharacterNameView : MonoBehaviour
    {
        [field: SerializeField] public PlayerCharacterName PlayerCharacterName { get; private set; }
    }
}
