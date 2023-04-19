using Unity.VisualScripting;
using UnityEngine;

namespace ProyectM2
{
    public class GameManager: MonoBehaviour
    {
        private static int _levelCurrency = 0;
        private static float _levelGas = 0;

        private void Start()
        {
            _levelCurrency = 0;
            _levelGas = 0;
        }

        public static void AddCurrency(int value)
        {
            _levelCurrency += value;
        }

        public static void AddGas(int value)
        {
            _levelGas += value;
        }
        public static void consumeGas(float value)
        {
            _levelGas -= value;
        }

        public void QuitGame()
        {
            _levelCurrency = 0;
            _levelGas = 0;
        }
    }
}
