using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class EnemySectionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.TriggerEvent("EnemyCutSceneStarted");
            }
        }
    }
}
