using Source.Enums;

namespace Source.Interfaces
{
    public interface IUpgradeable
    {
        CharacteristicStatus CharacteristicStatus { get; set; }
        
        void TryUpgrade(float value);

        float GetUpgradedCharacteristic();
    }
}