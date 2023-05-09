using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.GameLogic.BotsSpawners
{
    public abstract class SpawnersSwitcher : MonoBehaviour
    {
        [SerializeField] private List<BotsSpawner> _spawners = new();

        private readonly List<BotsSpawner> _workingSpawners = new();
        
        private Coroutine _coroutine;
        
        [field: SerializeField] public float Delay { get; private set; }

        protected virtual void OnEnable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff += OnTurnedOff;
        }

        protected virtual void OnDisable()
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

        protected IEnumerable<BotsSpawner> GetBotsSpawners()
        {
            return _spawners;
        }
        
        protected IEnumerable<BotsSpawner> GetWorkingBotsSpawners()
        {                                            
            return _workingSpawners;                        
        }

        protected BotsSpawner TryGetBotsSpawner(int index)
        {
            if (index < 0 || index >= _spawners.Count)
                return null;

            return _spawners[index];
        }

        protected int GetBotsSpawnersCount()
        {
            return _spawners.Count;
        }

        protected int GetWorkingBotsSpawnersCount()
        {
            return _workingSpawners.Count;
        }

        protected void AddWorkingSpawner(BotsSpawner botsSpawner)
        {
            _workingSpawners.Add(botsSpawner);
        }
        
        protected abstract void Activate();
    }
}