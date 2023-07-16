using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class Damaging : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable?.TakeDamage();
            Destroy(this);
        }
    }
}

