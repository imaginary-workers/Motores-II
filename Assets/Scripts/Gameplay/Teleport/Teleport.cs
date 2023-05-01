using ProyectM2.Managers;
using UnityEngine;
using ProyectM2.Persistence;
using System;

namespace ProyectM2.Gameplay.Teleport
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] bool _isInBonusLevel = false;
        [SerializeField] bool _teleportWasUsed = false;
        [SerializeField] Scene _scene;

        private void Awake()
        {
            if(SessionGameData.GetData("TeleportWasUsed") !=null)
                _teleportWasUsed = (bool)SessionGameData.GetData("TeleportWasUsed");

            if (SessionGameData.GetData("IsInBonusLevel") != null)
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");

            if (_teleportWasUsed && !_isInBonusLevel)
                gameObject.SetActive(false);

        }

        private void OnEnable()
        {
            EventManager.StartListening("TeleportToBonusLevel", TeleportToLevel);
            EventManager.StartListening("TeleportReturnToLevel", TeleportToLevel);
        }

        private void TeleportToLevel(object[] obj)
        {
            Debug.Log(obj[0]);
            Debug.Log((Scene)obj[0]);
            SceneManager.Instance.ChangeScene((Scene)obj[0]);
        }

        private void OnDisable()
        {
            EventManager.StopListening("TeleportToBonusLevel", TeleportToLevel);
            EventManager.StopListening("TeleportReturnToLevel", TeleportToLevel);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isInBonusLevel)
                {
                    SessionGameData.SaveData("TeleportWasUsed", true);
                    EventManager.TriggerEvent("TeleportToBonusLevel", _scene);
                    Debug.Log("Trigger");
                }
                else
                {
                    EventManager.TriggerEvent("TeleportReturnToLevel", _scene);
                }

                SessionGameData.SaveData("IsInBonusLevel", !_isInBonusLevel);
            }
        }
    }
}
