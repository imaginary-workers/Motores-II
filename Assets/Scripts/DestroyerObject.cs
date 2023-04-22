using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class DestroyerObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Traffic"))
            {
               Destroy(other.gameObject);
            }
        }
    }
}
