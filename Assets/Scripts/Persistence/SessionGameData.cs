using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Persistence
{
    public class SessionGameData : MonoBehaviour
    {

        private static Dictionary<string, object> sessionGameData = new();
        
        public static object GetData(string keyToReturnValue)
        {
            if (sessionGameData.ContainsKey(keyToReturnValue))
                return sessionGameData[keyToReturnValue];
            else 
                return null;
        }

        public static void SaveData(string keyToSave, object valueToSave)
        {
            if (sessionGameData.ContainsKey(keyToSave))
                sessionGameData[keyToSave] = valueToSave;
            else
                sessionGameData.Add(keyToSave, valueToSave);
        }

        public static void ResetData()
        {
            sessionGameData = new();
        }
    }
}
