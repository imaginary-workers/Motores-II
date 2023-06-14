using System;
using System.Collections.Generic;

namespace ProyectM2
{
    [Serializable]
    public class ValuesToSaveInJson
    {
        public int totalCurrencyOfPlayer;
        public int totalCurrencyGainOfPlayer;
        public int lastLevel;
        public float maxGas;
        public float timePlayed;
        public float musicVolume;
        public float soundVolume;
        public Dictionary<string, int> itemInInventory;
    }
}
