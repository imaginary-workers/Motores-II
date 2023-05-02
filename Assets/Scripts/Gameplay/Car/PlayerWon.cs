using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProyectM2.SO;

namespace ProyectM2
{
    public class PlayerWon : MonoBehaviour
    {
        [SerializeField] Events _events;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _events.InvokeEvent();
            }
        }
    }
}
