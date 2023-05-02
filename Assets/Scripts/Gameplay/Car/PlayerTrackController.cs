using System.Collections;
using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class PlayerTrackController : TrackController
    {
        [SerializeField] float _substracttGas = 0.1f;
        private bool _isInCutscene = false;
        private bool _onPause = true;

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
            if (_onPause) return;
            InputManager.CurrentInput.OnUpdate();
            GameManager.SubstractGas(_substracttGas * Time.deltaTime);
        }

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StartListening("EnemyCutSceneEnded", OnEnemyCutSceneEnded);
            EventManager.StartListening("OnPause", OnPauseHandler);
            EventManager.StartListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStarted);
            InputManager.CurrentInput.Horizontal += OnHorizontal;
        }

        private void OnEnemyDiedCutSceneStarted(object[] obj)
        {
            _isInCutscene = true;
            if (obj.Length == 0) return;
            var enemyTrack = (int) obj[0];
            if (enemyTrack == track)
            {
                if (enemyTrack == -1)
                {
                    MoveRight();
                }
                else if (enemyTrack == 1)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }

            StartCoroutine(CO_Wait());
        }

        private IEnumerator CO_Wait()
        {
            Debug.Log(_isInCutscene);
            yield return new WaitForSecondsRealtime(3f);
            _isInCutscene = false;
            Debug.Log(_isInCutscene);
        }

        private void OnPauseHandler(object[] obj)
        {
            if (obj.Length == 0) return;
            _onPause = (bool) obj[0];
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
            EventManager.StopListening("OnPause", OnPauseHandler);
            EventManager.StopListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStarted);
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
