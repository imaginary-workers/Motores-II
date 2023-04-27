using UnityEngine;

namespace ProyectM2.Gameplay
{
    public interface IBulletBehaviour
    {
        public void Update();
        public void OnTriggerEnter(Collider other);
    }
}