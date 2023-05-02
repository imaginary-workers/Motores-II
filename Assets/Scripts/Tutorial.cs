using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using ProyectM2.Persistence;

namespace ProyectM2
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] GameObject _canvasTutorial;
        void Start()
        {
            var teleportWasUsed = SessionGameData.GetData("TeleportWasUsed");
            if (teleportWasUsed == null || !(bool)teleportWasUsed)
            {
                _canvasTutorial.SetActive(true);
                StartCoroutine(Wait());
            }
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
