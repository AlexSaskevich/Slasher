using Lean.Localization;
using Source.Enums;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public class LanguageButton : UIButton
    {
        [SerializeField] private Language _language; 

        protected override void OnButtonClick()
        {
            base.OnButtonClick();

            LeanLocalization.SetCurrentLanguageAll(_language.ToString());
        }
    }
}