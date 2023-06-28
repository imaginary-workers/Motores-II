using ProyectM2.Assets.Scripts;
using ProyectM2.Car.Controller;
using ProyectM2.Gameplay.Car.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Shield : MonoBehaviour
    {

        [SerializeField] GameObject _shield;
        [SerializeField] Renderer _myrender;
        [SerializeField] BoxCollider _player;
        [SerializeField] AnimationController _ani;



        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle") || other.CompareTag("Traffic"))
            {
                _myrender.enabled = false;
                _player.enabled = false;
                _ani.DamagedPlayerAnimation();
                StartCoroutine(DisableCoroutine());
            }
        }
        IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(2.0f);

            _player.enabled = true;
            _shield.SetActive(false);

        }
    }
}
