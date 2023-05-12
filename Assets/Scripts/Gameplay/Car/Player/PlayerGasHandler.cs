using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerGasHandler: MonoBehaviour
    {
        [SerializeField] private float _break = 5f;
        [SerializeField] float _substracttGas = 1.5f;
        [SerializeField] private MoveController _moveController;
        private bool _isGasEmpty = false;

        #region Unity

        private void Update()
        {
            if (GameManager.isInCutScene) return;
            if (GameManager.isOnPause) return;
            if (GameManager._isInBonusLevel) return;
            GameManager.SubstractGas(_substracttGas * Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (!_isGasEmpty) return;
            _moveController.Speed -= _break * Time.deltaTime;
            if (_moveController.Speed <= 0)
            {
                EventManager.TriggerEvent("EndGameOver", GameOver.Gas);
                enabled = false;
            }
        }

        private void OnEnable()
        {
            EventManager.StartListening("StartGameOver", OnGameOverHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("StartGameOver", OnGameOverHandler);
        }
        #endregion

        private void OnGameOverHandler(object[] obj)
        {
            if (obj.Length <= 0) return;
            var gameOver = (GameOver)obj[0];
            if (gameOver == GameOver.Gas)
            {
                _isGasEmpty = true;
                return;
            }
            enabled = false;
        }
    }
}