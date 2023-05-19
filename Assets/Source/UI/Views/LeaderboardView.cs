using Source.Enums;
using Source.GameLogic.Timers;
using Source.Yandex;
using System.Collections.Generic;
using UnityEngine;

namespace Source.UI.Views
{
    public sealed class LeaderboardView : UIView
    {
        private const int MaxCharacterCount = 14;
        private const string AbbreviationSymbol = "...";

        [SerializeField] private LeaderboardPlayerView _prefab;
        [SerializeField] private Transform _parent;

        private readonly List<LeaderboardPlayerView> _spawnedPlayerViews = new();

        public void Create(List<LeaderboardPlayer> players, string leaderboardName)
        {
            Clear();

            foreach (var player in players)
            {
                var leaderboardPlayerView = Instantiate(_prefab, _parent);
                
                string playerName = TryTrimName(player.Name);

                leaderboardPlayerView.Init(player.Rank, playerName,
                    leaderboardName == LeaderboardName.SurvivalMode.ToString()
                        ? Timer.ConvertIntToTime(player.Score)
                        : player.Score.ToString());

                _spawnedPlayerViews.Add(leaderboardPlayerView);
            }
        }

        private string TryTrimName(string playerName)
        {
            string name;

            if (playerName.Length > MaxCharacterCount)
            {
                name = playerName.Remove(MaxCharacterCount);
                name += AbbreviationSymbol;
            }
            else
                name = playerName;

            return name;
        }

        private void Clear()
        {
            foreach (var playerView in _spawnedPlayerViews)
                Destroy(playerView.gameObject);

            _spawnedPlayerViews.Clear();
        }
    }
}