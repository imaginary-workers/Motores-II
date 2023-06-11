using ProyectM2.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace ProyectM2
{
    public class CurrencyBonus : MonoBehaviour
    {
        bool bonus;
        DataInt data;
        float _time;
        float _timeMax;

        void Update()
        {
            if (bonus)
            {
                _time += Time.deltaTime;
                data.value = 2;
                if(_time > _timeMax) bonus = false;
                _time = 0;
            }
            else data.value = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                bonus = true;
                Destroy(gameObject);
            }
        }
    }
}
