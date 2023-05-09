using Source.Skills;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.SkillViews
{
    public abstract class SkillView : UIView
    {
        private const float MinFillAmount = 0f;

        private TMP_Text _text;
        private Coroutine _coroutine;

        [field: SerializeField] public Image ImageToFill { get; protected set; }
        
        protected Skill Skill { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnDisable()
        {
            Skill.TimerStarted -= OnStarted;
        }

        protected virtual void Start()
        {
            if (_text != null)
                _text.text = string.Empty;

            ImageToFill.fillAmount = MinFillAmount;
        }

        public void Init(Skill skill)
        {
            Skill = skill;
            Skill.TimerStarted += OnStarted;
        }

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
                ImageToFill.fillAmount = totalTime / countdownTime;
                totalTime -= Time.deltaTime;

                if (_text != null)
                    _text.text = Mathf.RoundToInt(totalTime).ToString();

                yield return null;
            }

            if (_text != null)
                _text.text = string.Empty;

            ImageToFill.fillAmount = MinFillAmount;
        }
        
        protected abstract void OnStarted();
    }
}