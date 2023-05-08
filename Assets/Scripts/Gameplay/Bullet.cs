using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        private IBulletBehaviour _behaviour;

        public bool IsReturnable { get; private set; } = false;

        public void SetBehaviour(IBulletBehaviour behaviour, bool returnable = false)
        {
            _behaviour = behaviour;
            IsReturnable = returnable;
        }

        private void Update()
        {
            _behaviour.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            _behaviour.OnTriggerEnter(other);
        }

        private void OnDisable()
        {
            gameObject.layer = 12;
        }
    }
}
