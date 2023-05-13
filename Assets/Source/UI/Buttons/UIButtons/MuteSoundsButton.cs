using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class MuteSoundsButton : UIButton
    {
        [SerializeField] private AudioListener _audioListener;
        
        protected override void OnButtonClick()
        {
            Mute();
        }

        private void Mute()
        {
            _audioListener.enabled = !_audioListener.enabled;
        }
    }
}