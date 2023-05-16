using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class LoginAcceptButton : UIButton
    {
        [SerializeField] private ChoosingLeaderboardPanel _choosingLeaderboardPanel;
        [SerializeField] private LoginPanelView _loginPanelView;

        private void Start()
        {
            _choosingLeaderboardPanel.Hide();
        }

        public void OpenChoosingLeaderboardPanel()
        {
            _choosingLeaderboardPanel.Show();
            _loginPanelView.Hide();
        }
        
        protected override void OnButtonClick()
        {
            OpenChoosingLeaderboardPanel();
        }
    }
}