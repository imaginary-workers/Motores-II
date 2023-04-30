using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class SoundUI : MonoBehaviour
    {
        [SerializeField] AudioClip _currency;
        [SerializeField] AudioClip _gas;
        [SerializeField] AudioSource _source;

        private void OnEnable()
        {
            EventManager.StartListening("CurrencyModified", CurrencyPlay);
            EventManager.StartListening("GasModified", LevelGas);

        }
        private void OnDisable()
        {
            EventManager.StopListening("CurrencyModified", CurrencyPlay);
            EventManager.StopListening("CurrencyModified", LevelGas);
        }

        private void CurrencyPlay(object[] obj)
        {
            _source.clip = _currency;
            _source.Play();
        }
        private void LevelGas(object[] obj)
        {
            _source.clip = _gas;
            _source.Play();
        }
    }
}
