using System;
using System.Collections.Generic;
using Source.Enums;
using Source.GameLogic;

namespace Source.UI.Buttons.UIButtons
{
    public abstract class CharacterButton : UIButton
    {
        public event Action CharacterChanged;

        protected void ChooseCharacter(bool isScrollingForward)
        {
            var playerCharacterNames = new List<int>
            {
                (int)PlayerCharacterName.Biker, (int)PlayerCharacterName.Medic, (int)PlayerCharacterName.Ninja,
                (int)PlayerCharacterName.Death
            };
            
            var currentPlayerCharacter = GameProgressSaver.GetCurrentCharacterIndex();
            var index = playerCharacterNames.IndexOf(currentPlayerCharacter);

            int newIndex;

            if (isScrollingForward)
                newIndex = index == playerCharacterNames.Count - 1 ? 0 : index + 1;
            else
                newIndex = index == 0 ? playerCharacterNames.Count - 1 : index - 1;

            GameProgressSaver.SetCurrentCharacterIndex(playerCharacterNames[newIndex]);
            
            CharacterChanged?.Invoke();
        }
    }
}