using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProyectM2.Gameplay;
using System;

namespace ProyectM2.Managers.Levels
{
    public class Sections : MonoBehaviour, IObservable
    {
        IObserver _myObserver;

        [SerializeField] float _distanceToCreateSection;
        [SerializeField] float _distanceToDeleteSection;

        Transform _nextPivot;
        Transform _lastPivot;
        GameObject _myPlayer;
        bool _createSectionNotifed = false;

        private void Start()
        {
            _nextPivot = transform.Find("DistanceToCreateSectionPivot");
            _lastPivot = transform.Find("DeleteSectionPivot");

            _myPlayer = GameManager.player;
        }

        private void Update()
        {
            if (_myPlayer != null)
                CheckDistanceFromPlayer(_nextPivot, _lastPivot, _myPlayer);
            else
                _myPlayer = GameManager.player;

        }

        void CheckDistanceFromPlayer(Transform nextPivot, Transform lastPivot, GameObject player)
        {
            Vector3 dirToNextPivot = player.transform.position - nextPivot.transform.position;
            Vector3 dirToPastPivot = lastPivot.transform.position - player.transform.position;
            if (dirToNextPivot.sqrMagnitude <= (_distanceToCreateSection * _distanceToCreateSection) && !_createSectionNotifed)
            {
                NotifyToObservers("CreateSection");   
                _createSectionNotifed = true;
            }
            if (dirToPastPivot.sqrMagnitude >= (_distanceToDeleteSection * _distanceToDeleteSection))
            {
                Destroy(gameObject);
            }
        }

        public void NotifyToObservers(string action)
        {
            _myObserver?.Notify(action);
        }

        public void Suscribe(IObserver obs)
        {
            _myObserver = obs;
        }

        public void Unsuscribe(IObserver obs)
        {
            if (obs == _myObserver)
                _myObserver = null;
        }
    }
}
