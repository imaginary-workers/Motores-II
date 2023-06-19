using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class EfectDectected : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particle;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("entre a particle");
            Vector3 contactPoint = other.transform.position;
            ParticleSystem particleInstance = Instantiate(_particle, contactPoint, Quaternion.identity);
            particleInstance.Play();

            Destroy(particleInstance.gameObject, particleInstance.main.duration);
        }
    }
}
