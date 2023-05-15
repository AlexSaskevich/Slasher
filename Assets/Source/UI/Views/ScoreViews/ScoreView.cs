using Source.GameLogic.Scores;
using TMPro;
using UnityEngine;

namespace Source.UI.Views.ScoreViews
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class ScoreView : MonoBehaviour
    {
        [field: SerializeField] protected TMP_Text ScoreText { get; private set; }
        protected Score Score { get; private set; }

        public void Init(Score score)
        {
            Score = score;
            
            Init();
        }

        protected abstract void Init();
    }
}