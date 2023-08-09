using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay.Recolectables
{
    public class GasRecolectable : MonoBehaviour
    {
        [SerializeField] DataIntObservable _gasValue;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                MyGameManager.AddGas(_gasValue.value);
                Destroy(gameObject);
            }
        }
    }
}
