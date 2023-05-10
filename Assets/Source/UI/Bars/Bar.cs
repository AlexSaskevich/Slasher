using Source.Interfaces;
using Source.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Bars
{
    public abstract class Bar : MonoBehaviour
    {
        private const float MaxFillAmount = 1;

        [SerializeField] private float _fillingSpeed;
        [SerializeField] private Image _image;

        private Coroutine _coroutine;

        protected PlayerHealth PlayerHealth { get; private set; }

        private void OnDisable()
        {
            PlayerHealth.Died -= OnDied;
        }

        private void Start()
        {
            _image.fillAmount = MaxFillAmount;
        }

        public virtual void Init(PlayerHealth playerHealth, IUpgradeable upgradeable = null)
        {
            PlayerHealth = playerHealth;
            PlayerHealth.Died += OnDied;
        }

        protected abstract void OnValueChanged();

        protected void StartChangeFillAmount(float value)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeFillAmount(value));
        }

        private IEnumerator ChangeFillAmount(float value)
        {
            while (_image.fillAmount != value)
            {
                _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, value, _fillingSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void OnDied()
        {
            StartChangeFillAmount(0);
        }
    }
}