using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class PlayerTrackController : TrackController
    {
        [SerializeField] float _substracttGas = 0.1f;
        private bool _isInCutscene = false;

        private void Start()
        {
            Transform coche = transform.GetChild(0);

            if(coche != null)
            {
                coche.gameObject.tag = "Player";
            }
        }

        private void Update()
        {
            if (_isInCutscene) return;
            InputManager.CurrentInput.OnUpdate();
            GameManager.SubstractGas(_substracttGas * Time.deltaTime);
        }

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StartListening("EnemyCutSceneEnded", OnEnemyCutSceneEnded);
            InputManager.CurrentInput.Horizontal += OnHorizontal;
        }

        private void OnEnemyCutSceneEnded(object[] obj)
        {
            _isInCutscene = false;
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            _isInCutscene = true;
            if (track != 0)
            {
                if (track == 1)
                    MoveLeft();
                else MoveRight();
            }
        }

        private void OnDisable()
        {
            InputManager.CurrentInput.Horizontal -= OnHorizontal;
            EventManager.StopListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StopListening("EnemyCutSceneEnded", OnEnemyCutSceneEnded);
        }

        private void OnHorizontal(int hor)
        {
            if (hor > 0)
                MoveRight();
            else
                MoveLeft();
        }
    }
}