using System.Collections.Generic;
using Source.GameLogic.Timers;
using Source.Yandex;
using UnityEngine;

namespace Source.UI.Views
{
    public sealed class LeaderboardView : UIView
    {
        [SerializeField] private LeaderboardPlayerView _prefab;

        private readonly List<LeaderboardPlayerView> _spawnedPlayerViews = new();

        public void Create(List<LeaderboardPlayer> players)
        {
            Clear();                                      

            foreach (var player in players)
            {
                var leaderboardPlayerView = Instantiate(_prefab, transform);
                leaderboardPlayerView.Init(player.Name, Timer.ConvertIntToTime(player.Score));

                _spawnedPlayerViews.Add(leaderboardPlayerView);
            }
        }

        private void Clear()
        {
            foreach (var playerView in _spawnedPlayerViews)
                Destroy(playerView.gameObject);
        
            _spawnedPlayerViews.Clear();
        }
    }
}