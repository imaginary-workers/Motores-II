using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class AnimManager : MonoBehaviour
    {
        /*        
        EL ENTITY SERIA EL AUTO

        [SerializeField] Entity _entity;
         */
        
        [SerializeField] Animator _myAnim;
        Dictionary<string, Action> _events = new Dictionary<string, Action>();
        int turnLeftParameterId;
        int turnRightrameterId;
        int crashParameterId;

        private void Start()
        {
            //_entity.Health.OnDeath += DeathAnimation;
            turnLeftParameterId = Animator.StringToHash("TurnRight");
            turnRightrameterId = Animator.StringToHash("TurnLeft");
            crashParameterId = Animator.StringToHash("Crash");
        }

        public void TurnLeft()
        {
            _myAnim.SetTrigger(turnLeftParameterId);
        }

        public void TurnRight()
        {
            _myAnim.SetTrigger(turnRightrameterId);
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
