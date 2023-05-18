using System.Collections.Generic;
using Agava.YandexGames;
using Source.UI.Views;
using UnityEngine;

namespace Source.Yandex
{
    public sealed class Leaderboard : MonoBehaviour
    {
        private const string AnonymousName = "Anonymous";
        private const int MinPlayersCount = 1;
        private const int MaxPlayersCount = 5;

        [SerializeField] private LeaderboardView _leaderboardView;
        
        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        public static void AddPlayer(int score, string leaderboardName)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Agava.YandexGames.Leaderboard.GetPlayerEntry(leaderboardName, _ =>
            {
                Agava.YandexGames.Leaderboard.SetScore(leaderboardName, score);
            });
        }
        
        public void Fill(string leaderboardName)
        {
            _leaderboardPlayers.Clear();

            if (PlayerAccount.IsAuthorized == false)
                return;
            
            Agava.YandexGames.Leaderboard.GetEntries(leaderboardName, result =>
            {
                var results = result.entries.Length;
                results = Mathf.Clamp(results, MinPlayersCount, MaxPlayersCount);

                for (var i = 0; i < results; i++)
                {
                    var score = result.entries[i].score;
                    var playerName = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(playerName))
                        playerName = AnonymousName;

                    _leaderboardPlayers.Add(new LeaderboardPlayer(playerName, score));
                }

                _leaderboardView.Create(_leaderboardPlayers, leaderboardName);
            });
        }
    }
}