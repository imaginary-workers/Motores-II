using ProyectM2.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Obstacle : MonoBehaviour
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

