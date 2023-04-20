using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.GameLogic
{
    public abstract class SpawnersSwitcher : MonoBehaviour
    {
        [SerializeField] private List<BotsSpawner> _spawners = new();

        private readonly List<BotsSpawner> _workingSpawners = new();
        
        private Coroutine _coroutine;
        
        [field: SerializeField] public float Delay { get; private set; }
        
        private void OnEnable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff += OnTurnedOff;
        }

        private void OnDisable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff -= OnTurnedOff;
        }

        protected void ResetSpawners()
        {
            foreach (var spawner in _workingSpawners)
                spawner.ResetOptions();
            
            _workingSpawners.Clear();
        }
        
        private void OnTurnedOff()
        {
            if (_workingSpawners.All(spawner => spawner.CurrentWave == null) == false)
                return;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitTime());
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(Delay);
            
            Activate();
        }

        protected List<BotsSpawner> GetBotsSpawners()
        {
            return _spawners;
        }
        
        protected List<BotsSpawner> GetWorkingBotsSpawners()
        {                                            
            return _workingSpawners;                        
        }                         
        
        protected abstract void Activate();
    }
}