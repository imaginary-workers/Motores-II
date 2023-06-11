using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2
{
    public class TimePlayed : Singleton<TimePlayed>
    {
        public float TotalTimePlayed => _timePlayed;
        private float _timePlayed;
        ValuesToSaveInJson _myJsonData;


        private void Start()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            _timePlayed = _myJsonData.timePlayed;

        }

        private void Update()
        {
            _timePlayed += Time.deltaTime;
        }

        public void ResetTime()
        {
            _timePlayed = 0f;
        }
    }
}
