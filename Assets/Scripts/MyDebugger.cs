using UnityEngine;

namespace ProyectM2
{
    public class MyDebugger : MonoBehaviour
    {
        public void Log(object objectToPrint)
        {

            Debug.Log($"<color=#00F7FF>Log</color> " + objectToPrint);
        }

        public void Warning(object objectToPrint)
        {
            Debug.Log($"<color=#FFEE00>Warning</color> " + objectToPrint);
            
        }
        
        public void Error(object objectToPrint)
        {
            Debug.Log($"<color=#FF0000>Warning</color> " + objectToPrint);
        }

        public void Ok(object objectToPrint)
        {
            Debug.Log($"<color=#4FFF00>OK</color> " + objectToPrint);
                
        }
    }
}



