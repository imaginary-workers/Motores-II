using ProyectM2.Scenes;
using UnityEngine;
using ProyectM2.Persistence;


namespace ProyectM2.Gameplay.Recolectables
{
    public class TeleportRecolectable : MonoBehaviour
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
                SessionGameData.SaveData("IsInBonusLevel", !_isInBonusLevel);
                
                if (!_isInBonusLevel)
                {
                    SessionGameData.SaveData("TeleportWasUsed", true);
                    EventManager.TriggerEvent("TeleportToBonusLevel");
                }
                else
                {
                    EventManager.TriggerEvent("TeleportReturnToLevel");
                }
                EventManager.TriggerEvent("ChangeScene", _scene);
            }
        }
    }
}
