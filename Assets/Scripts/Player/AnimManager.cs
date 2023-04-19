using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Car
{
    public class AnimManager : MonoBehaviour
    {
        [SerializeField] Animator _myAnim;
        Dictionary<string, Action> _events = new Dictionary<string, Action>();
        int turnLeftParameterId;
        int turnRightParameterId;
        int crashParameterId;

        private void Start()
        {
            //_entity.Health.OnDeath += DeathAnimation;
            turnRightParameterId = Animator.StringToHash("TurnRight");
            turnLeftParameterId = Animator.StringToHash("TurnLeft");
            crashParameterId = Animator.StringToHash("Crash");
        }

        public void TurnLeftAnimation()
        {
            _myAnim.SetTrigger(turnLeftParameterId);
        }

        public void TurnRightAnimation()
        {
            _myAnim.SetTrigger(turnRightParameterId);
        }

        public void DeathAnimation()
        {
            _myAnim.SetTrigger(crashParameterId);
            this.enabled = false;
        }

        public void SetEvent(string key, Action method)
        {
            if (_events.ContainsKey(key))
                return;
            else
                _events.Add(key, method);
        }

        public void RemoveEvent(string key)
        {
            if (_events.ContainsKey(key))
                _events.Remove(key);
        }

        public void ExecuteEvent(string key)
        {
            if (_events.ContainsKey(key))
                _events[key]();
        }
    }

}
