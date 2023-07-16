using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay.Recolectables
{
    public class GasRecolectable : MonoBehaviour
    {
        [SerializeField] DataIntObservable _gasValue;
        [SerializeField] GameObject _gas;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                MyGameManager.AddGas(_gasValue.value);
                Destroy(_gas);
            }
        }
    }
}
