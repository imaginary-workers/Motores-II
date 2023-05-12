using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2
{
    public class Damaging : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.GetComponent<IDamageable>()?.TakeDamage();
        }
    }
}

