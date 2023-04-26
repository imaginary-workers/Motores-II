using System;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _velocity;
        [SerializeField] float _destroy;
        private IBulletBehaviour _behaviour;

        public void SetBehaviour(IBulletBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        private void Update()
        {
            _behaviour.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            _behaviour.OnTriggerEnter(other);
        }
    }
}
