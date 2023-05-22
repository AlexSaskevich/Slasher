namespace Source.Turntables
{
    public sealed class ButtonTurntable : Turntable
    {
        public void PlayButtonSound()
        {
            SetRandomPitch();
            SetVolume();
            PlaySound();
        }
    }
}