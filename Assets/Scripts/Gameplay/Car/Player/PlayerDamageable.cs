using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        private bool _isInBonusLevel = false;

        private void Start()
        {
            if (SessionGameData.GetData("IsInBonusLevel") != null)
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");            
        }
        public void TakeDamage()
        {
            EventManager.TriggerEvent("StartGameOver", _isInBonusLevel? GameOver.Bonus : GameOver.Crash);
        }
    }
}