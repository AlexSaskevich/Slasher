namespace Source.UI.Buttons.UIButtons
{
    public sealed class NextCharacterButton : CharacterButton
    {
        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            
            ChooseCharacter(true);
        }
    }
}