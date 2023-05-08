using System;
using System.Collections;
using UnityEngine;
using ProyectM2.Persistence;

namespace ProyectM2
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] GameObject _canvasTutorial;
        [SerializeField] GameObject _canvasFirebackTutorial;
        void Start()
        {
            var teleportWasUsed = SessionGameData.GetData("TeleportWasUsed");
            if (teleportWasUsed == null || !(bool)teleportWasUsed)
            {
                _canvasTutorial.SetActive(true);
                StartCoroutine(Wait());
            }
        }

        private void OnEnable()
        {
            EventManager.StartListening("FirebackTutorial", OnFirebackTutorialHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("FirebackTutorial", OnFirebackTutorialHandler);
        }

        private void OnFirebackTutorialHandler(object[] obj)
        {
            if (obj.Length == 0) return;
            _canvasFirebackTutorial.SetActive((bool) obj[0]);
        }

        void AfterTutorial()
        {
            _canvasTutorial.SetActive(false);
        }

        IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(6);
            AfterTutorial();
        }
    }
}
