using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    public sealed class LeaderboardPlayerView : UIView
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _score;

        public void Init(int rank, string playerName, string score)
        {
            _rank.text = rank.ToString();
            _playerName.text = playerName;
            _score.text = score;
        }
    }
}