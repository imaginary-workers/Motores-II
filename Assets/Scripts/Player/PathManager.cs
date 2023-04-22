using UnityEngine;

namespace ProyectM2.Player
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] float _speed = 1;
        [SerializeField] private string _targetTag = "PathTarget";
        [SerializeField] GameObject[] _pathTargets;
        private int _indexTarget = 0;
        private bool _canMove = true;

        private void Start()
        {
            SetForwardToTarget(_indexTarget);
        }

        private void SetForwardToTarget(int indexTarget)
        {
            transform.forward = (_pathTargets[indexTarget].transform.position - transform.position).normalized;
        }

        private void Update()
        {
            if (!_canMove) return;
            transform.position += (_pathTargets[_indexTarget].transform.position - transform.position).normalized *
                                  _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_targetTag))
            {
                _indexTarget++;
                if (_indexTarget >= _pathTargets.Length)
                {
                    //TODO es el ultimo, asi que debe avisar que termino el nivel.
                    _canMove = false;
                    return;
                }
                SetForwardToTarget(_indexTarget);
            }
        }
    }
}

