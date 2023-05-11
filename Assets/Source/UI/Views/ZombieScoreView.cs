using System;
using Source.GameLogic;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class ZombieScoreView : MonoBehaviour
    {
        protected ZombieScore ZombieScore { get; private set; }
        protected TMP_Text Score { get; private set; }

        private void Awake()
        {
            Score = GetComponent<TMP_Text>();
        }

        public void Init(ZombieScore zombieScore)
        {
            ZombieScore = zombieScore;
            
            Init();
        }

        protected abstract void Init();
    }
}