using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class FollowCam : MonoBehaviour
    {
        [SerializeField] GameObject _player;
        [SerializeField,Range(0,20)] int _camY;
        [SerializeField,Range(-20,20)] int _camZ;

        private void Update()
        {
            Vector3 _posPlayer = _player.transform.position;
            transform.position = new Vector3(_posPlayer.x,_posPlayer.y + _camY, _posPlayer.z - _camZ);
        }
    }
}
