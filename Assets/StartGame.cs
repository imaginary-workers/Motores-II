using System.Collections;
using UnityEngine;

namespace ProyectM2
{
    public class StartGame : MonoBehaviour
    {
        private bool activated = false;
        private void Update()
        {
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            Time.timeScale = 0;
            yield return null;
            Destroy(gameObject);
        }
    }
}
