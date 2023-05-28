using UnityEngine;

namespace ProyectM2.Levels
{
    public class LevelInfinityHandler : MonoBehaviour
    {
        [SerializeField] LevelManager _levelManager;
        [SerializeField] Section _levelSection;

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", NewInfinitiveSection);
        }


        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", NewInfinitiveSection);
        }

        void NewInfinitiveSection(object[] obj)
        {
           _levelManager.SetFirstSection(_levelSection);
        }
    }
}