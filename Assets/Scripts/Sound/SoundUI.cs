using ProyectM2.Gameplay;
using UnityEngine;
using ProyectM2.Music;
using ProyectM2.Persistence;

namespace ProyectM2.Sound
{
    public class SoundUI : MonoBehaviour
    {
        [SerializeField] AudioClip _currency;
        [SerializeField] AudioClip _gas;
        [SerializeField] AudioSource _source;
        [SerializeField] AudioClip _gameOver;
        [SerializeField] AudioClip _gameOverBonusLevel;
        [SerializeField] AudioClip _win;
        [SerializeField] AudioClip _teleport;

        private void OnEnable()
        {
            EventManager.StartListening("CurrencyModified", CurrencyPlay);
            EventManager.StartListening("GasModified", LevelGas);
            EventManager.StartListening("EndGameOver", GameOverSound);
            EventManager.StartListening("Won", WonSound);
            EventManager.StartListening("GameOverBonusLevel", BonusGameOverSound);
            EventManager.StartListening("TeleportToBonusLevel", Teleport);
            EventManager.StartListening("TeleportReturnToLevel", Teleport);
        }

        private void OnDisable()
        {
            EventManager.StopListening("CurrencyModified", CurrencyPlay);
            EventManager.StopListening("GasModified", LevelGas);
            EventManager.StopListening("EndGameOver", GameOverSound);
            EventManager.StopListening("Won", WonSound);
            EventManager.StopListening("GameOverBonusLevel", BonusGameOverSound);
            EventManager.StopListening("TeleportToBonusLevel", Teleport);
            EventManager.StopListening("TeleportReturnToLevel", Teleport);
        }

        private void CurrencyPlay(object[] obj)
        {
            _source.clip = _currency;
            _source.Play();
        }

        private void LevelGas(object[] obj)
        {
            _source.clip = _gas;
            _source.Play();
        }

        private void Teleport(object[] obj)
        {
            _source.clip = _teleport;
            _source.Play();
        }

        private void BonusGameOverSound(object[] obj)
        {
        }

        private void WonSound(object[] obj)
        {
            MusicManager.Instance.PlayMusic(_win);
        }

        private void GameOverSound(object[] obj)
        {
            if (obj.Length <= 0 ) return;
            if ((GameOver)obj[0] == Gameplay.GameOver.Bonus)
            {
                _source.clip = _gameOverBonusLevel;
                _source.Play();
                return;
            }
            MusicManager.Instance.PlayMusic(_gameOver);
        }
    }
}
