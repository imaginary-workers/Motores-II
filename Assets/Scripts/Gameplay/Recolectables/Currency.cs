using UnityEngine;
using ProyectM2.SO;

namespace ProyectM2.Gameplay.Recolectables
{
    public class Currency : MonoBehaviour
    {
        [SerializeField] GameObject currencyPrefab;
        [SerializeField] DataInt _currencyValue;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            { 
                GameManager.AddCurrency(_currencyValue.value);
                Destroy(currencyPrefab);
            }
        }
    }
}
