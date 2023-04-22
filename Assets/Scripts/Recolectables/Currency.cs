using UnityEngine;
using ProyectM2.SO;

namespace ProyectM2
{
    public class Currency : MonoBehaviour
    {
        [SerializeField] GameObject currencyPrefab;
        [SerializeField] DataInt _currencyValue;

        private void OnTriggerEnter(Collider other)
        {
                Debug.Log(other.gameObject.name + " el tag es de " + other.tag);
            if (other.CompareTag("Player"))
            { 
                GameManager.AddCurrency(_currencyValue.value);
                Destroy(currencyPrefab);
            }
        }
    }
}
