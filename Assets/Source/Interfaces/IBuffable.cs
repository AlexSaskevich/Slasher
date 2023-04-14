namespace Source.Interfaces
{
    public interface IBuffable
    {
        public bool IsBuffed { get; }

        public void AddModifier(float modifier);

        public void RemoveModifier(float modifier);
    }
}