using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Managers.Levels
{
    public class LevelManagerListener : MonoBehaviour
    {
        [SerializeField] LevelManager _levelManager;

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", NewInfinitiveSection);
            EventManager.StartListening("EnemyDiedCutSceneStarted", DisableInfinitiveSection);
            EventManager.StartListening("TeleportToBonusLevel", SaveLastIndex);
            EventManager.StartListening("TeleportReturnToLevel", SaveLastIndex);
        }


        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", NewInfinitiveSection);
            EventManager.StopListening("EnemyDiedCutSceneStarted", DisableInfinitiveSection);
            EventManager.StopListening("TeleportToBonusLevel", SaveLastIndex);
            EventManager.StopListening("TeleportReturnToLevel", SaveLastIndex);

        }

        private void SaveLastIndex(object[] obj)
        {
            SessionGameData.SaveData("LastSectionIndex", _levelManager._currentIndex - 1);
        }

        void NewInfinitiveSection(object[] obj)
        {
            _levelManager._isInInfinitiveSection = true;
        }

        void DisableInfinitiveSection(object[] obj)
        {
            _levelManager._isInInfinitiveSection = false;
        }
    }
}