namespace Source.Turntables
{
    public sealed class BuffTurntable : Turntable
    {
        public void PlayBuffSound()
        {
            SetRandomPitch();
            SetVolume();
            PlaySound();
        }
    }
}