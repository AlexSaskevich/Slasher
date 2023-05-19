using Source.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class MuteSoundsButton : UIButton
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private Image _imageToHide;
        [SerializeField] private Image _imageToShow;

        private bool _isMuted;

        private void Start()
        {
            _isMuted = SoundMuter.IsMuted;
            
            if (_isMuted == false)
            {
                _imageToHide.gameObject.SetActive(false);
                _imageToShow.gameObject.SetActive(true);
            }
            else
            {
                _imageToHide.gameObject.SetActive(true);
                _imageToShow.gameObject.SetActive(false);
            }
        }

        protected override void OnButtonClick()
        {
            if (_isMuted)
                UnMute();
            else
                Mute();
        }

        private void Mute()
        {
            _isMuted = true;
            _audioListener.enabled = false;
            
            _imageToHide.gameObject.SetActive(true);
            _imageToShow.gameObject.SetActive(false);
            
            SoundMuter.Mute();
        }
        
        private void UnMute()
        {
            _isMuted = false;
            _audioListener.enabled = true;
            
            _imageToHide.gameObject.SetActive(false);
            _imageToShow.gameObject.SetActive(true);
            
            SoundMuter.Unmute();
        }
    }
}