using Source.GameLogic;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class ZombieScoreView : MonoBehaviour
    {
        [field: SerializeField] protected TMP_Text Score { get; private set; }
        protected ZombieScore ZombieScore { get; private set; }

        public void Init(ZombieScore zombieScore)
        {
            ZombieScore = zombieScore;
            
            Init();
        }

        protected abstract void Init();
    }
}