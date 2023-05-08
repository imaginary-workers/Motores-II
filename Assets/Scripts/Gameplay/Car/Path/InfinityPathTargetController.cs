using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    public class InfinityPathTargetController : MonoBehaviour
    {
        [SerializeField] private float _distanceFromPlayer = 10f;
        private bool _infinite = false;
        private GameObject _player;

#if UNITY_EDITOR
        
        [SerializeField] private MeshRenderer _renderer;
#endif
        private void Awake()
        {
#if UNITY_EDITOR
            _renderer.material.color = Color.red;
#endif
        }

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", NewInfiniteSection);
            EventManager.StartListening("SceneLoadComplete", LookForPlayer);
        }

        private void LookForPlayer(object[] obj)
        {
            _player = GameManager.player;
            if (_player == null)
            {
                var playerRoot = GameObject.Find("Player");
                if (playerRoot != null)
                {
                    _player = playerRoot.GetComponentInChildren<PlayerTrackController>().gameObject;
                }
            }

            _player = _player.transform.root.gameObject;
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", NewInfiniteSection);
            EventManager.StopListening("SceneLoadComplete", LookForPlayer);
        }

        private void NewInfiniteSection(object[] obj)
        {
            _infinite = true;
        }

        private void LateUpdate()
        {
            if (!_infinite) return;
            transform.position = _player.transform.position + _player.transform.forward * _distanceFromPlayer;
        }
    }
}