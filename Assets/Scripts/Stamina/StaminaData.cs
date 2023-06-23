using ProyectM2.Persistence;
using System;
using UnityEngine;

namespace ProyectM2
{
    public class StaminaData : MonoBehaviour
    {
        public int CurrentStamina => _currentStamina;
        private int _currentStamina;

        public DateTime NextStaminaTime => _nextStaminaTime;
        private DateTime _nextStaminaTime;

        public DateTime LastStaminaTime => _lastStaminaTime;
        private DateTime _lastStaminaTime;
        public void SaveStaminaData(int currentStamina, string nextStaminaTime, string lastStaminaTime)
        {
            DataPersistance.Instance.UpdateStaminaData(currentStamina, nextStaminaTime, lastStaminaTime);
        }

        public void LoadStaminaData()
        {
            var _myJsonData = DataPersistance.Instance.LoadGame();
            _currentStamina = _myJsonData.stamina;
            _nextStaminaTime = StringToDateTime(_myJsonData.nextStaminaTime);
            _lastStaminaTime = StringToDateTime(_myJsonData.lastStaminaTime);
        }

        DateTime StringToDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.Now;
            }

            return DateTime.Parse(date);
        }
    }
}
