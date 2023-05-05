using Source.Enums;

namespace Source.Interfaces
{
    public interface IUpgradeable
    {
        CharacteristicStatus CharacteristicStatus { get; }
        float MaxValue { get; }
        
        void TryUpgrade(float value);

        float GetUpgradedCharacteristic();
    }
}