using Source.GameLogic;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class SecondGameModeHighestScoreView : MonoBehaviour
    {
        private TMP_Text _score;

        private void Awake()
        {
            _score = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(GameProgressSaver.GetTimeText()))
                _score.text = "00 : 00";
            else
                _score.text = GameProgressSaver.GetTimeText();
        }
    }
}