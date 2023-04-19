using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Currency : MonoBehaviour
    {
        [SerializeField] GameObject currencyPrefab;
        [SerializeField] int _currencyValue;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {               
                BorrarMonedas.AddCurrency();

                //Gamemanager.AddCurrency(int 1);
                Destroy(currencyPrefab);
            }
        }
    }
}
