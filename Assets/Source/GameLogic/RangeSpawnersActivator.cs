using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class RangeSpawnersActivator : MonoBehaviour
    {
        [SerializeField] private List<BotsSpawner> _rangeBotsSpawners = new();
        [SerializeField] private int _workingSpawnersCount;
        [SerializeField] private float _timeToActivate;

        private readonly List<BotsSpawner> _workingSpawners = new();

        private Coroutine _coroutine;
        private float _timer;

        private void OnEnable()
        {
            foreach (var rangeBotsSpawner in _rangeBotsSpawners)
                rangeBotsSpawner.TurnedOff += OnTurnedOff;
        }

        private void OnDisable()
        {
            foreach (var rangeBotsSpawner in _rangeBotsSpawners)
                rangeBotsSpawner.TurnedOff -= OnTurnedOff;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < _timeToActivate || _workingSpawners.Count > 0)
                return;
            
            Activate();
            _timer = 0;
        }

        private void SetSpawners()
        {
            while (_workingSpawnersCount > _rangeBotsSpawners.Count)
                _workingSpawnersCount--;

            if (_workingSpawnersCount == 0)
                return;
            
            for (var i = 0; i < _workingSpawnersCount; i++)
            {
                var randomSpawnerNumber = Random.Range(0, _rangeBotsSpawners.Count);

                while (_workingSpawners.Any(spawner => spawner == _rangeBotsSpawners[randomSpawnerNumber]))
                    randomSpawnerNumber = Random.Range(0, _rangeBotsSpawners.Count);

                _workingSpawners.Add(_rangeBotsSpawners[randomSpawnerNumber]);
            }
        }

        private void Activate()
        {
            ResetSpawners();
            SetSpawners();

            foreach (var workingSpawner in _workingSpawners)
                workingSpawner.gameObject.SetActive(true);
        }

        private void ResetSpawners()
        {
            foreach (var workingSpawner in _workingSpawners)
                workingSpawner.ResetOptions();
            
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
            yield return new WaitForSeconds(_timeToActivate);
            
            ResetSpawners();
        }
    }
}