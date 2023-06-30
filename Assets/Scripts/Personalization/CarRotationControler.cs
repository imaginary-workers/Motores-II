using UnityEngine;

namespace ProyectM2.Personalization
{
    public class CarRotationControler : MonoBehaviour
    {
        public GameObject objectToDetect;        
        public MonoBehaviour scriptToActivateAutomatic;
        public MonoBehaviour scriptToActivateManual;

        private void OnEnable()
        {
            scriptToActivateAutomatic.enabled = true;
            objectToDetect.SetActive(true);
            if (scriptToActivateAutomatic != null)
            {
                scriptToActivateAutomatic.enabled = false;
                scriptToActivateManual.enabled = true;
            }
        }

        private void OnDisable()
        {
            objectToDetect.SetActive(false);
            if (scriptToActivateAutomatic != null)
            {
                scriptToActivateAutomatic.enabled = true;
                scriptToActivateManual.enabled = false;
            }
        }
    }
}
