using ProyectM2.Gameplay.Car.Controller;
using ProyectM2.Gameplay.Car.Player;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class InfinityPathTargetController : MonoBehaviour
    {
        [SerializeField] private MoveController _moveController;
        private GameObject _player;

        #region Unity
        private void OnEnable()
        {
            CutSceneManager.Instance.Subscribe("EnemyArrival", CutSceneState.Started, NewInfiniteSection);
            EventManager.StartListening("SceneLoadComplete", LookForPlayer);
        }

        private void OnDisable()
        {
            CutSceneManager.Instance.Unsubscribe("EnemyArrival", CutSceneState.Started, NewInfiniteSection);
            EventManager.StopListening("SceneLoadComplete", LookForPlayer);
        }
        #endregion

        private void LookForPlayer(object[] obj)
        {
            _player = MyGameManager.player;
            if (_player == null)
            {
                var playerRoot = GameObject.FindObjectOfType<PlayerInputHorizontalMovement>();
                if (playerRoot != null)
                {
                    _player = playerRoot.GetComponentInChildren<TrackController>().gameObject;
                }
            }

            _player = _player.transform.root.gameObject;
        }

        private void NewInfiniteSection()
        {
            _moveController.Speed = _player.GetComponent<MoveController>().Speed;
            _moveController.Direction = _player.transform.forward;
        }
    }
}