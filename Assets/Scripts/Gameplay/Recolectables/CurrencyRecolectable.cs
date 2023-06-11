using UnityEngine;
using ProyectM2.SO;

namespace ProyectM2.Gameplay.Recolectables
{
    public class CurrencyRecolectable : MonoBehaviour
    {
        [SerializeField] GameObject currencyPrefab;
        [SerializeField] DataInt _currencyValue;
        CurrencyBonus _bonus;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.AddCurrency(_currencyValue.value,_bonus);
                Destroy(currencyPrefab);
            }
        }
    }
}
