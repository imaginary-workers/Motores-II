using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class EnemySectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyTimelinePrefab;
        public float distancia = 10f;
        public float ancho = 1f;
        public float alto = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerTransform = GameManager._player.transform;
                var instantiate = Instantiate(_enemyTimelinePrefab, playerTransform.position + playerTransform.forward * 2,
                    Quaternion.identity);
                //===========
                Vector3 direction = instantiate.transform.forward;
                Vector3 origen = instantiate.transform.position;

                var hits = Physics.BoxCastAll(origen, new Vector3(ancho / 2, alto / 2, distancia / 2), direction);

                if (hits.Length > 0)
                {
                    float minDistance = Mathf.Infinity;
                    GameObject closestPathTarget = null;

                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.CompareTag("PathTarget"))
                        {
                            if (hit.distance < minDistance)
                            {
                                minDistance = hit.distance;
                                closestPathTarget = hit.collider.gameObject;
                            }
                        }
                    }

                    if (closestPathTarget != null)
                    {
                        instantiate.GetComponentInChildren<PathManager>().SetCurrentPathTarget(closestPathTarget);
                    }
                }
            }
        }
    }
}
