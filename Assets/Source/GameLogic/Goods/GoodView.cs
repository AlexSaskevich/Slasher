using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.GameLogic.Goods
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
            _goodBlinder.Good.LevelMaxed += OnLevelMaxed;
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _goodBlinder.Good.LevelMaxed -= OnLevelMaxed;
        }

        private void Start()
        {
            ShowLevel();
        }

        private void OnClick()
        {
            _goodBlinder.Good.TryBuy();
            ShowLevel();
        }

        private void ShowLevel()
        {
            _level.text = _goodBlinder.Good.Level.ToString();
        }

        private void OnLevelMaxed()
        {
            _button.interactable = false;
        }
    }
}