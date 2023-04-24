using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProyectM2.Gameplay;

namespace ProyectM2
{
    public class Sections : MonoBehaviour, IObservable
    {
        IObserver _myObserver;

        //cuando le tiene que decir que tiene que crear una Seccion?
        // 1) Cuando el player pase por un pivot o cuando este dentro de un range
        // Tengo que llamar al player

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
            _myPlayer = GameManager._player;

            Debug.Log(transform.name);


        }

        private void Update()
        {
            CheckDistanceFromPlayer(_nextPivot, _lastPivot, _myPlayer);
        }

        void CheckDistanceFromPlayer(Transform nextPivot, Transform lastPivot, GameObject player)
        {
            Vector3 dirToNextPivot = player.transform.position - nextPivot.transform.position;
            Vector3 dirToPastPivot = lastPivot.transform.position - player.transform.position;
            if (dirToNextPivot.sqrMagnitude <= (_distanceToCreateSection * _distanceToCreateSection) && !_createSectionNotifed)
            {
                NotifyToObservers("Create Section");
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
