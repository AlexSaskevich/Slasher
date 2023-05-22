namespace Source.Turntables
{
    public sealed class UltimateTurntable : Turntable
    {
        public void PlayBoostSound()
        {
            SetRandomPitch();
            SetVolume();
            PlaySound();
        }
    }
}