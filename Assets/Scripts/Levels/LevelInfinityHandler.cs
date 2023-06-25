using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2.Levels
{
    public class LevelInfinityHandler : MonoBehaviour
    {
        [SerializeField] LevelManager _levelManager;
        [SerializeField] Section _levelSection;

        private void OnEnable()
        {
            CutSceneManager.Instance.Subscribe("EnemyArrival", CutSceneState.Started, NewInfinitiveSection);
        }


        private void OnDisable()
        {
            CutSceneManager.Instance.Unsubscribe("EnemyArrival", CutSceneState.Started, NewInfinitiveSection);
        }

        void NewInfinitiveSection()
        {
           _levelManager.SetFirstSection(_levelSection);
        }
    }
}