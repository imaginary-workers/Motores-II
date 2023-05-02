using ProyectM2.SO;using UnityEngine;

namespace ProyectM2
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] Events _events;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("choque");
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("con el player");

                _events.InvokeEvent();
            }
        }
    }
}

