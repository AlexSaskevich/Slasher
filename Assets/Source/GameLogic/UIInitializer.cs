using System;
using System.Collections.Generic;
using System.Linq;
using Source.Combo;
using Source.Enums;
using Source.GameLogic.Boosters;
using Source.GameLogic.BotsSpawners;
using Source.GameLogic.Scores;
using Source.GameLogic.Timers;
using Source.InputSource;
using Source.Player;
using Source.Skills;
using Source.UI.Bars;
using Source.UI.Buttons.ControlButtons;
using Source.UI.Buttons.UIButtons;
using Source.UI.Panels;
using Source.UI.Views;
using Source.UI.Views.ScoreViews;
using Source.UI.Views.SkillViews;
using Source.UI.Views.SkillViews.CooldownViews;
using Source.UI.Views.SkillViews.DurationViews;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class UIInitializer : MonoBehaviour
    {
        [SerializeField] private List<Bar> _bars = new();
        [SerializeField] private PlayerWalletView _playerWalletView;
        [SerializeField] private BuyCharacterButton _buyCharacterButton;
        [SerializeField] private MoneyButton _moneyButton;
        [SerializeField] private TimerBlinder _timerBlinder;
        [SerializeField] private RangeSpawnersSwitcher _rangeSpawnersSwitcher;
        [SerializeField] private DeathScreen _deathScreen;
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private RegenerationButton _regenerationButton;
        [SerializeField] private List<ScoreView> _scoreViews;
        [SerializeField] private AttackButton _attackButton;
        [SerializeField] private List<BoostBlinder> _boostBlinders = new();
        [SerializeField] private List<SkillView> _skillViews = new();

        public void Init(PlayerCharacter playerCharacter)
        {
            if (playerCharacter.TryGetComponent(out PlayerAgility playerAgility) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerMana playerMana) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Buff buff) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Ultimate ultimate) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Roll roll) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out InputSwitcher inputSwitcher) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerCombo playerCombo) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Animator animator) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerDeathAnimation playerDeathAnimation) == false)
                throw new ArgumentNullException();
            
            foreach (var bar in _bars.Where(bar => bar != null))
            {
                switch (bar)
                {
                    case PlayerHealthBar:
                        bar.Init(playerHealth);
                        break;
                    
                    case PlayerAgilityBar:
                        bar.Init(playerHealth, playerAgility);
                        break;
                    
                    case PlayerManaBar:
                        bar.Init(playerHealth, playerMana);
                        break;
                    
                    default:
                        throw new ArgumentNullException();
                }
            }
            
            if (_playerWalletView != null)
                _playerWalletView.Init(playerWallet);

            if (_buyCharacterButton != null)
                _buyCharacterButton.Init(playerWallet);

            if (_moneyButton != null)
                _moneyButton.Init(playerWallet);

            if (_timerBlinder != null && _rangeSpawnersSwitcher != null)
                _timerBlinder.Init(_rangeSpawnersSwitcher.Delay, playerHealth);

            if (_deathScreen != null)
                _deathScreen.Init(inputSwitcher, playerHealth, playerDeathAnimation.GetLenght());

            if (_endScreen != null)
                _endScreen.Init(inputSwitcher);

            if (_regenerationButton != null)
            {
                _regenerationButton.Init(playerHealth, playerCombo, inputSwitcher.InputSource, animator, playerMana,
                    playerAgility);
            }

            if (playerCharacter.TryGetComponent(out ZombieScore score) &&
                playerCharacter.TryGetComponent(out TimeModeScore timeScore) && _scoreViews.Count > 0)
            {
                foreach (var scoreView in _scoreViews)
                {
                    if (scoreView == null)
                        return;

                    switch (scoreView)
                    {
                        case ZombieCurrentScoreView or ZombieHighestScoreView:
                            scoreView.Init(score);
                            break;

                        case TimeModeCurrentScoreView or TimeModeHighestScoreView:
                            scoreView.Init(timeScore);
                            break;

                        default:
                            throw new ArgumentNullException();
                    }
                }
            }

            foreach (var skillView in _skillViews.Where(skillView => skillView != null))
            {
                switch (skillView)
                {
                    case BuffDurationView or BuffCooldownPCView or BuffCooldownMobileView:
                        skillView.Init(buff, inputSwitcher.InputSource, ultimate, buff, roll);
                        break;
                    
                    case UltimateCooldownPCView or UltimateCooldownMobileView or UltimateDurationView:
                        skillView.Init(ultimate, inputSwitcher.InputSource, ultimate, buff, roll);
                        break;
                    
                    case RollCooldownMobileView or RollDurationView or RollCooldownPCView:
                        skillView.Init(roll, inputSwitcher.InputSource, ultimate, buff, roll);
                        break;
                }
            }

            if (_attackButton != null)
                _attackButton.Init(ultimate, buff, roll, playerCombo);

            foreach (var boostBlinder in _boostBlinders.Where(boostBlinder => boostBlinder != null))
            {
                switch (boostBlinder.GoodStatus)
                {
                    case GoodStatus.HealthUpgradeable:
                        boostBlinder.Init(playerWallet, playerHealth, playerCharacter.PlayerCharacterName);
                        break;

                    case GoodStatus.ManaUpgradeable:
                        boostBlinder.Init(playerWallet, playerMana, playerCharacter.PlayerCharacterName);
                        break;

                    case GoodStatus.AgilityUpgradeable:
                        boostBlinder.Init(playerWallet, playerAgility, playerCharacter.PlayerCharacterName);
                        break;

                    default:
                        throw new ArgumentNullException();
                }

                if (boostBlinder.TryGetComponent(out BoostView boostView) == false)
                    throw new ArgumentNullException();

                boostView.Init(playerWallet);
            }
        }
    }
}