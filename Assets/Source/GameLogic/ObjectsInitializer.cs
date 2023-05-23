using Cinemachine;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class ObjectsInitializer : MonoBehaviour
    {
        [SerializeField] private SpawnerInitializer _spawnerInitializer;
        [SerializeField] private UIInitializer _uiInitializer;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public void InitObjects(PlayerCharacter playerCharacter)
        {
            if (_virtualCamera != null)
            {
                _virtualCamera.Follow = playerCharacter.transform;
                _virtualCamera.LookAt = playerCharacter.transform;
            }

            _uiInitializer.Init(playerCharacter);
            _spawnerInitializer.InitSpawners(playerCharacter);
        }
    }
}