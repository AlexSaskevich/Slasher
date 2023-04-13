namespace Source.Interfaces
{
    public interface ISkill
    {
        public float Cooldown { get; }

        public void Activate();
    }
}