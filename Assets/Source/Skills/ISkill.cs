namespace Source.Skills
{
    public interface ISkill
    {
        public float Cooldown { get; }

        public void Activate();
    }
}