using ProyectM2.Gameplay;
using UnityEngine;
using ProyectM2.Persistence;

namespace ProyectM2
{
    public class Obstacle : MonoBehaviour
    {
        private bool _isInBonusLevel = false;

        private void Start()
        {
            if (SessionGameData.GetData("IsInBonusLevel") != null)
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EventManager.TriggerEvent("StartGameOver", _isInBonusLevel? GameOver.Bonus : GameOver.Crash);
            }
        }
    }
}

