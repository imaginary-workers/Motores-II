using System.Collections;
using ProyectM2.Car.Controller;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerAnimationHandler: MonoBehaviour
    {
        [SerializeField] private float _secondsToLose = 2f;
        [SerializeField] private AnimationController _animationController;

        private void OnEnable()
        {
            EventManager.StartListening("StartGameOver", OnStartGameOverHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("StartGameOver", OnStartGameOverHandler);
        }

        private void OnStartGameOverHandler(object[] obj)
        {
            if (obj.Length <=0) return;
            var gameOver = (GameOver) obj[0];
            if (gameOver != GameOver.Crash && gameOver != GameOver.Bonus) return;
            _animationController.DeathAnimation();
            StartCoroutine(WaitToGameOver(gameOver));
        }

        private IEnumerator WaitToGameOver(GameOver gameOver)
        {
            yield return new WaitForSeconds(_secondsToLose);
            EventManager.TriggerEvent("EndGameOver", gameOver);
        }
    }
}