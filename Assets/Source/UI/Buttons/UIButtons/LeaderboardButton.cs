using Agava.YandexGames;
using Source.Enums;
using Source.UI.Views;
using UnityEngine;
using Leaderboard = Source.Yandex.Leaderboard;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class LeaderboardButton : UIButton
    {
        [SerializeField] private LeaderboardName _leaderboardName;
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private ChoosingLeaderboardPanel _choosingLeaderboardPanel;

        private void Start()
        {
            _leaderboardView.Hide();
        }

        protected override void OnButtonClick()
        {
            OpenLeaderboard();
        }

        private void OpenLeaderboard()
        {
            PlayerAccount.Authorize();

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.RequestPersonalProfileDataPermission();

            if (PlayerAccount.IsAuthorized == false)
                return;

            _choosingLeaderboardPanel.Hide();
            _leaderboardView.Show();
            _leaderboard.Fill(_leaderboardName.ToString());
        }
    }
}