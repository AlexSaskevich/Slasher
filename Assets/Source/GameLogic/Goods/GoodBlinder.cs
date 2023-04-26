using Source.Enums;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Goods
{
    public sealed class GoodBlinder : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _upgradeableBehaviour;
        [SerializeField] private int _price;
        [SerializeField] private int _increasingValue;
        [SerializeField] private GoodStatus _goodStatus;
        [SerializeField] private PlayerWallet _playerWallet;

        private IUpgradeable _upgradeable;
        
        public Good Good { get; private set; }

        private void OnValidate()
        {
            if (_upgradeableBehaviour && _upgradeableBehaviour is IUpgradeable == false)
                Debug.Log($"{nameof(_upgradeableBehaviour)} needs to implement {nameof(IUpgradeable)}");
        }

        private void Awake()
        {
            _upgradeable = (IUpgradeable)_upgradeableBehaviour;
            Good = new Good(GameProgressSaver.GetGoodLevel(_goodStatus), _increasingValue, _upgradeable, _price,
                _playerWallet, _goodStatus);
        }
    }
}