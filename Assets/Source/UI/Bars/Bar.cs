using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private float _fillingSpeed;
    [SerializeField] private Image _image;

    private Coroutine _coroutine;

    protected void Initialize(float maxValue)
    {
        _image.fillAmount = maxValue;
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
}