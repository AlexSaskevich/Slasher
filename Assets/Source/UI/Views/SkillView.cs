using Source.Skills;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    public abstract class SkillView : UIView
    {
        protected const float MinFillAmount = 0f;
        protected const float MaxFillAmount = 1f;

        private TMP_Text _text;
        private Coroutine _coroutine;
        
        [field: SerializeField] public Image Image { get; protected set; }
        public Buff Buff { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnDisable()
        {
            Buff.TimerStarted -= OnStarted;
        }

        protected virtual void Start()
        {
            _text.text = string.Empty;
        }

        public void Init(Buff buff)
        {
            Buff = buff;
            Buff.TimerStarted += OnStarted;
        }

        protected abstract void OnStarted();

        protected void StartFillImage(float countdownTime)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(FillImage(countdownTime));
        }

        private IEnumerator FillImage(float countdownTime)
        {
            float totalTime = countdownTime;

            while (totalTime >= 0)
            {
                Image.fillAmount = totalTime / countdownTime;
                totalTime -= Time.deltaTime;
                _text.text = Mathf.RoundToInt(totalTime).ToString();
                yield return null;
            }

            _text.text = string.Empty;
        }
    }
}