namespace Source.Interfaces
{
    public interface IMoveable
    {
        float DefaultSpeed { get; }

        void Move(float directionX, float directionZ);
    }
}