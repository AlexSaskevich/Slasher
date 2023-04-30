namespace Source.UI.Buttons.UIButtons
{
    public sealed class PreviousCharacterButton : CharacterButton
    {
        protected override void OnButtonClick()
        {
            ChooseCharacter(false);
        }
    }
}