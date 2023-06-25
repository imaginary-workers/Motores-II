using System.Collections;
using ProyectM2.Gameplay;
using UnityEngine;
using ProyectM2.Persistence;

namespace ProyectM2
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] GameObject _canvasTutorial;
        [SerializeField] GameObject _canvasFirebackTutorial;
        [SerializeField] GameObject _canvasEnemyTutorial;

        void Start()
        {
            var teleportWasUsed = SessionGameData.GetData("TeleportWasUsed");
            if (teleportWasUsed == null || !(bool)teleportWasUsed)
            {
                _canvasTutorial.SetActive(true);
                StartCoroutine(WaitToDesactive(_canvasTutorial, 6f));
            }
        }

        private void OnEnable()
        {
            EventManager.StartListening("FirebackTutorial", OnFirebackTutorialHandler);
            CutSceneManager.Instance.Subscribe("EnemyArrival", CutSceneState.Started, OnStartTutorialEnemyHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("FirebackTutorial", OnFirebackTutorialHandler);
            CutSceneManager.Instance.Unsubscribe("EnemyArrival", CutSceneState.Started, OnStartTutorialEnemyHandler);
        }

        private void OnFirebackTutorialHandler(object[] obj)
        {
            if (obj.Length == 0) return;
            var firebackTutorialActived = (bool) obj[0];
            _canvasFirebackTutorial.SetActive(firebackTutorialActived);
            Time.timeScale = firebackTutorialActived? .1f : 1f;
        }

        private void OnStartTutorialEnemyHandler()
        {
            _canvasEnemyTutorial.SetActive(true);
            StartCoroutine(WaitToDesactive(_canvasEnemyTutorial, 4f));
        }

        IEnumerator WaitToDesactive(GameObject canvas, float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            canvas.SetActive(false);
        }
    }
}
