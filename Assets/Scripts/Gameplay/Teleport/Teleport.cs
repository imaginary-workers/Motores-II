using ProyectM2.Managers;
using UnityEngine;
using ProyectM2.Persistence;

namespace ProyectM2.Gameplay.Teleport
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] bool _isInBonusLevel = false;
        [SerializeField] bool _teleportWasUsed = false;
        [SerializeField] Scene _scene;

        private void Awake()
        {

            if (SessionGameData.GetData("TeleportWasUsed") !=null)
                _teleportWasUsed = (bool)SessionGameData.GetData("TeleportWasUsed");

            if (SessionGameData.GetData("IsInBonusLevel") != null)
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");

            if (_teleportWasUsed && !_isInBonusLevel)
                gameObject.SetActive(false);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isInBonusLevel)
                {
                    SessionGameData.SaveData("TeleportWasUsed", true);
                    EventManager.TriggerEvent("TeleportToBonusLevel", _scene);
                }
                else
                {
                    EventManager.TriggerEvent("TeleportReturnToLevel", _scene);
                }
                SessionGameData.SaveData("IsInBonusLevel", !_isInBonusLevel);
                SceneManager.Instance.ChangeScene(_scene);
            }
        }
    }
}
