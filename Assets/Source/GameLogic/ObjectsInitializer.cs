using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class ObjectsInitializer : MonoBehaviour
    {
        [SerializeField] private SpawnerInitializer _spawnerInitializer;
        [SerializeField] private UIInitializer _uiInitializer;
        [SerializeField] private PlayerFollower _playerFollower;
        
        public void InitObjects(PlayerCharacter playerCharacter)
        {
            if (_playerFollower != null)
                _playerFollower.Init(playerCharacter.transform);

            _uiInitializer.Init(playerCharacter);
            _spawnerInitializer.InitSpawners(playerCharacter);
        }
    }
}