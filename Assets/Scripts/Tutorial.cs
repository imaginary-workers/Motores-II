using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace ProyectM2
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] GameObject _canvasTutorial;
        void Start()
        {
            _canvasTutorial.SetActive(true);
            StartCoroutine(Wait());
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
