namespace Source.Interfaces
{
    public interface IBuffable
    {
        bool IsBuffed { get; }

        void AddModifier(float modifier);

        void RemoveModifier(float modifier);
    }
}