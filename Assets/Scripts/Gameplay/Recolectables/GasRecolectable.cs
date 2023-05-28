using ProyectM2.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Gameplay.Recolectables
{
    public class GasRecolectable : MonoBehaviour
    {
        [SerializeField] DataInt _gasValue;
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
