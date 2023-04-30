using ProyectM2.Managers;
using UnityEngine;

namespace ProyectM2.Gameplay.Teleport
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] bool IsInBonusLevel = false;
        [SerializeField] Scene _scene;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!IsInBonusLevel)
                    EventManager.TriggerEvent("TeleportToBonusLevel", _scene);
                else
                    EventManager.TriggerEvent("TeleportReturnToLevel", _scene);
            }
        }
    }
}
