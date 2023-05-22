namespace Source.Turntables
{
    public sealed class RollTurntable : Turntable
    {
        public void PlayRollSound()
        {
            SetRandomPitch();
            SetVolume();
            PlaySound();
        }
    }
}