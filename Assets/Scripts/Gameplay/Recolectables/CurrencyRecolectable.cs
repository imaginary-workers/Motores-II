using UnityEngine;
using ProyectM2.SO;

namespace ProyectM2.Gameplay.Recolectables
{
    public class CurrencyRecolectable : MonoBehaviour
    {
        [SerializeField] GameObject currencyPrefab;
        [SerializeField] DataIntObservable _currencyValue;
        [SerializeField] DataIntObservable _playerLevelCurrency;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerLevelCurrency.value += _currencyValue.value;
                Destroy(currencyPrefab);
            }
        }
    }
}
