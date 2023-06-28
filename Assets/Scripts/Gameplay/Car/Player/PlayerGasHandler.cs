using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerGasHandler : MonoBehaviour, IActivatable
    {
        [SerializeField] private float _break = 5f;
        [SerializeField] float _substracttGas = 1.5f;
        [SerializeField] private MoveController _moveController;
        private bool _isGasEmpty = false;
        private bool _isActive = false;

        #region Unity


        private void Update()
        {
            if (!_isActive) return;
            if (MyGameManager.isInBonusLevel) return;
            MyGameManager.SubstractGas(_substracttGas * Time.deltaTime);
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
            ScreenManager.Instance.Subscribe(this);
            EventManager.StartListening("StartGameOver", OnGameOverHandler);
        }

        private void OnDisable()
        {
            ScreenManager.Instance.Unsubscribe(this);
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