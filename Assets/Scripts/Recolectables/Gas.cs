using ProyectM2.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Gas : MonoBehaviour
    {
        [SerializeField] Data _gasValue;
        [SerializeField] GameObject _gas;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameManager.AddGas(_gasValue.value);
                Destroy(_gas);
            }
        }
    }
}
