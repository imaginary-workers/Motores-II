using System;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Path
{
    /*
     *Level Manager:
     * Escuchar cuando muera el Enemy
     *      quitar el ciclo infinito y colocar la seccion 2
     *
     * Path Infinito 
     * Se aleje del player en la dir de su forward
     * Escuchar cuando muera el Enemy
     *  Cuando se cree la seccion 2, asignarse su nextpathtarget al primero de la seccion 2
     *  Dejar de alejarse del player
     * Escuchar cuando muera el Player
     *  Dejar de alejarse del player
     * 
     */
    public class InfinityPathTargetController : MonoBehaviour
    {
        [SerializeField] private PathTargetInfo _pathTargetInfo;
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

        private void Start()
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

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", NewInfiniteSection);
            EventManager.StartListening("EnemyDiedCutSceneStarted", DisableInfiniteSection);
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", NewInfiniteSection);
            EventManager.StopListening("EnemyDiedCutSceneStarted", DisableInfiniteSection);
        }

        private void DisableInfiniteSection(object[] obj)
        {
            _infinite = false;
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