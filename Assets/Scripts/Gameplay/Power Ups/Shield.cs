using ProyectM2.Car.Controller;
using System.Collections;
using UnityEngine;

namespace ProyectM2
{
    public class Shield : MonoBehaviour
    {

        [SerializeField] private GameObject _shield;
        [SerializeField] private BoxCollider _player;
        [SerializeField] private AnimationController _ani;



        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle") || other.CompareTag("Traffic"))
            {
                _player.enabled = false;
                _ani.DamagedPlayerAnimation();
                StartCoroutine(DisableCoroutine());
                _shield.SetActive(false);
            }
        }
        IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(2.0f);

            _player.enabled = true;

        }
    }
}
