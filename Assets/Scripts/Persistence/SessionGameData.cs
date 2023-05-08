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
        
        public static bool TryGetData(string key, out object value)
        {
            if (sessionGameData.TryGetValue(key, out value))
            {
                return true;
            }
            else
            {
                value = null;
                return false;
            }
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
            sessionGameData.Clear();
        }
    }
}
