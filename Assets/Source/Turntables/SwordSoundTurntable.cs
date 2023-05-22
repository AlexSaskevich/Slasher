﻿namespace Source.Turntables
{
    public sealed class SwordSoundTurntable : Turntable
    {
        public void PlaySwordSound()
        {
            SetRandomPitch();
            SetVolume();
            PlaySound();
        }
    }
}