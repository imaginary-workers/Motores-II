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
            if (SessionGameData.GetData("TeleportWasUsed") != null)
            {
                if (!(bool)SessionGameData.GetData("TeleportWasUsed"))
                {
                    _canvasTutorial.SetActive(true);
                    StartCoroutine(Wait());
                }

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
