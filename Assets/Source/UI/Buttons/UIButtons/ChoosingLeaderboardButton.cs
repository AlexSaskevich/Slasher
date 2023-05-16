using Agava.YandexGames;
using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class ChoosingLeaderboardButton : UIButton
    {
        [SerializeField] private LoginPanelView _loginPanelView;
        [SerializeField] private LoginAcceptButton _loginAcceptButton;

        private void Start()
        {
            _loginPanelView.Hide();
        }

        protected override void OnButtonClick()
        {
            TryOpenLeaderboards();
        }

        private void TryOpenLeaderboards()
        {
            if (PlayerAccount.IsAuthorized == false)
                _loginPanelView.Show();
            else
                _loginAcceptButton.OpenChoosingLeaderboardPanel();
        }
    }
}