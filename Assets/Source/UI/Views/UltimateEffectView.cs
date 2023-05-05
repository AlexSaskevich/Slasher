using Source.Skills;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    public class UltimateEffectView : MonoBehaviour
    {
        private const float MinFillAmount = 0f;

        private TMP_Text _text;
        private Coroutine _coroutine;

        [field: SerializeField] public Image Image { get; private set; }
        public Ultimate Ultimate { get; private set; }

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnDisable()
        {
            Ultimate.TimerStarted -= OnStarted;
        }

        private void Start()
        {
            _text.text = string.Empty;
            Image.fillAmount = MinFillAmount;
        }

        public void Init(Ultimate ultimate)
        {
            Ultimate = ultimate;
            Ultimate.TimerStarted += OnStarted;
        }

        private void OnStarted()
        {
            StartFillImage(Ultimate.Duration);
        }

        private void StartFillImage(float countdownTime)
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