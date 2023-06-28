using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class CarRotationControler : MonoBehaviour
    {
        public GameObject objectToDetect;        
        public MonoBehaviour scriptToActivate;
        public MonoBehaviour scriptToActivate2;

        private void OnEnable()
        {
            objectToDetect.SetActive(true);
            if (scriptToActivate != null)
            {
                scriptToActivate.enabled = false;
                scriptToActivate2.enabled = true;
            }
        }

        private void OnDisable()
        {
            objectToDetect.SetActive(false);
            if (scriptToActivate != null)
            {
                scriptToActivate.enabled = true;
                scriptToActivate2.enabled = false;
            }
        }
    }
}
