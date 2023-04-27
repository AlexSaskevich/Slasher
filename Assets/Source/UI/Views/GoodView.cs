using Source.GameLogic.Goods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    [RequireComponent(typeof(GoodBlinder),typeof(Button))]
    public sealed class GoodView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _level;
        
        private GoodBlinder _goodBlinder;
        private Button _button;

        private void Awake()
        {
            _goodBlinder = GetComponent<GoodBlinder>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void Start()
        {
            ShowLevel();
            TryTurnOff();
        }

        private void OnClick()
        {
            _goodBlinder.Good.TryBuy();
            ShowLevel();
            TryTurnOff();
        }

        private void ShowLevel()
        {
            _level.text = _goodBlinder.Good.Level.ToString();
        }

        private void TryTurnOff()
        {
            if (_goodBlinder.Good.IsMaxLevel())
                _button.interactable = false;
        }
    }
}