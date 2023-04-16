using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class FalsePlayer : MonoBehaviour
    {
        [SerializeField] GameObject _player;
        [SerializeField] float _speed = 1;

        private void Update()
        {
            Vector3 _posPlayer = _player.transform.position;

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}

