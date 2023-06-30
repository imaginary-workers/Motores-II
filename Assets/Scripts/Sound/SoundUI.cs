using ProyectM2.Gameplay;
using UnityEngine;
using ProyectM2.Music;
using ProyectM2.SO;

namespace ProyectM2.Sound
{
    public class SoundUI : MonoBehaviour, IActivatable
    {
        [SerializeField] private AudioClip _currency;
        [SerializeField] private AudioClip _gas;
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _gameOver;
        [SerializeField] private AudioClip _gameOverBonusLevel;
        [SerializeField] private AudioClip _win;
        [SerializeField] private AudioClip _teleport;

        [SerializeField] private DataIntObservable _levelCurrency;
        private bool _isActive = false;

        private void OnEnable()
        {
            ScreenManager.Instance.Subscribe(this);
            EventManager.StartListening("GasModified", LevelGas);
            EventManager.StartListening("EndGameOver", GameOverSound);
            EventManager.StartListening("Won", WonSound);
            EventManager.StartListening("TeleportToBonusLevel", Teleport);
            EventManager.StartListening("TeleportReturnToLevel", Teleport);
            _levelCurrency.Subscribe(CurrencyPlay);
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Unsubscribe(this);
            EventManager.StopListening("GasModified", LevelGas);
            EventManager.StopListening("EndGameOver", GameOverSound);
            EventManager.StopListening("Won", WonSound);
            EventManager.StopListening("TeleportToBonusLevel", Teleport);
            EventManager.StopListening("TeleportReturnToLevel", Teleport);
            _levelCurrency.Unsubscribe(CurrencyPlay);
        }

        private void CurrencyPlay()
        {
            if (!_isActive) return;
            _source.clip = _currency;
            _source.Play();
        }

        private void LevelGas(object[] obj)
        {
            if (!_isActive) return;
            _source.clip = _gas;
            _source.Play();
        }

        private void Teleport(object[] obj)
        {
            if (!_isActive) return;
            _source.clip = _teleport;
            _source.Play();
        }

        private void WonSound(object[] obj)
        {
            if (!_isActive) return;
            MusicManager.Instance.PlayMusic(_win);
        }

        private void GameOverSound(object[] obj)
        {
            if (!_isActive) return;
            if (obj.Length <= 0 ) return;
            if ((GameOver)obj[0] == GameOver.Bonus)
            {
                _source.clip = _gameOverBonusLevel;
                _source.Play();
                return;
            }
            MusicManager.Instance.PlayMusic(_gameOver);
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
            
        }
    }
}
