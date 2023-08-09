using ProyectM2.Car.Controller;
using System.Collections;
using UnityEngine;

namespace ProyectM2
{
    public class Shield : MonoBehaviour
    {

        [SerializeField] private GameObject _shield;
        [SerializeField] private Renderer _myrender;
        [SerializeField] private BoxCollider _player;
        [SerializeField] private AnimationController _ani;



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
