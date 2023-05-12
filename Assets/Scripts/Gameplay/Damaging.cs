using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2
{
    public class Damaging : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable == null) return;
            Debug.Log(gameObject.name + " => " + other.gameObject.name);
            damageable?.TakeDamage();
            Destroy(this);
        }
    }
}

