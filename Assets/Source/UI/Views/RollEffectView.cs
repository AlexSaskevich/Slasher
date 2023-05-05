using Source.Constants;
using Source.Enums;
using Source.Player;
using Source.Skills;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    public class RollEffectView : MonoBehaviour
    {
        private const float MinFillAmount = 0f;

        private TMP_Text _text;
        private Coroutine _coroutine;
        private float _duration;

        [field: SerializeField] public Image Image { get; private set; }
        public Roll Roll { get; private set; }

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnDisable()
        {
            Roll.TimerStarted -= OnStarted;
        }

        private void Start()
        {
            _text.text = string.Empty;
            Image.fillAmount = MinFillAmount;
        }

        public void Init(Roll roll)
        {
            Roll = roll;
            Roll.TimerStarted += OnStarted;
            var player = roll.GetComponent<PlayerCharacter>().PlayerCharacterName;
            _duration = player == PlayerCharacterName.Ninja ? AnimationConstants.RollWithJumpDuration : AnimationConstants.RollDuration;
        }

        private void OnStarted()
        {
            StartFillImage(_duration);
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
                yield return null;
            }

            _text.text = string.Empty;
            Image.fillAmount = MinFillAmount;
        }
    }
}