﻿using System;
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

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", NewInfiniteSection);
            EventManager.StartListening("SceneLoadComplete", LookForPlayer);
            // EventManager.StartListening("EnemyDiedCutSceneStarted", DisableInfiniteSection);
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
            // EventManager.StopListening("EnemyDiedCutSceneStarted", DisableInfiniteSection);
        }

        private void DisableInfiniteSection(object[] obj)
        {
            // _infinite = false;
            // var pathTargetInfos = FindObjectsOfType<PathTargetInfo>();
            // var minDistance = float.PositiveInfinity;
            // PathTargetInfo closestTargetInfo = null;
            // foreach (var targetInfo in pathTargetInfos)
            // {
            //     if (closestTargetInfo == null)
            //     {
            //         closestTargetInfo = targetInfo;
            //         minDistance = (transform.position - closestTargetInfo.transform.position).magnitude;
            //         continue;
            //     }
            //     
            //     var distance = (transform.position - closestTargetInfo.transform.position).magnitude;
            //     if (distance < minDistance)
            //     {
            //         minDistance = distance;
            //         closestTargetInfo = targetInfo;
            //     }
            // }
            //
            // if (closestTargetInfo == null)
            // {
            //     Debug.LogError("No encontro ningun PathTargetInfo");
            //     return;
            // }
            //
            // _pathTargetInfo.NextPathTarget = closestTargetInfo.gameObject;
        }

        private void NewInfiniteSection(object[] obj)
        {
            _infinite = true;
        }

        private void LateUpdate()
        {
            if (!_infinite) return;
            if (_player == null)
            {
                Debug.Log("Player es null");
            }
            else
            {
                Debug.Log("Player NOT null");
            }
            transform.position = _player.transform.position + _player.transform.forward * _distanceFromPlayer;
        }
    }
}