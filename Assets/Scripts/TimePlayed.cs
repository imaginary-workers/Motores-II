using ProyectM2.Persistence;
using System.Collections;
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
            Debug.Log("Load TimePlayed " + _timePlayed);
            StartCoroutine(CO_SaveDataPlayed());
        }

        private void Update()
        {
            _timePlayed += Time.deltaTime;
        }

        public void ResetTime()
        {
            _timePlayed = 0f;
        }

        private IEnumerator CO_SaveDataPlayed()
        {
            while (true)
            {
                DataPersistance.Instance.UpdateTime(_timePlayed);
                yield return new WaitForSecondsRealtime(5f);
            }
        }
    }
}
