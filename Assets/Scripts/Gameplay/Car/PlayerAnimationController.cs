using System.Collections;
using ProyectM2.Car;
using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class PlayerAnimationController: AnimationController
    {
        [SerializeField] private float _secondsToLose = 2f;

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
            DeathAnimation();
            StartCoroutine(WaitToGameOver(gameOver));
        }

        private IEnumerator WaitToGameOver(GameOver gameOver)
        {
            yield return new WaitForSeconds(_secondsToLose);
            EventManager.TriggerEvent("EndGameOver", gameOver);
        }
    }
}