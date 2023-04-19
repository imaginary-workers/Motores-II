using UnityEngine;

namespace ProyectM2
{
    public class GameManager: MonoBehaviour
    {
        private static int levelCurrency = 0;

        private void Start()
        {
            levelCurrency = 0;
        }

        public static void AddCurrency(int value)
        {
            levelCurrency += value;
        }

        public void QuitGame()
        {
            levelCurrency = 0;
        }
    }
}
