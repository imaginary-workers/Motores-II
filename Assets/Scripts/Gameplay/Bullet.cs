using System;
using UnityEngine;

namespace ProyectM2
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _velocity;
        Vector2 _position;
        [SerializeField] float _destroy;

        private void OnEnable()
        {
            _destroy = 0;
        }

        private void Update()
        {
            _destroy += Time.deltaTime;
            transform.position += transform.forward * _velocity * Time.deltaTime;
            if (_destroy >= 10)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
