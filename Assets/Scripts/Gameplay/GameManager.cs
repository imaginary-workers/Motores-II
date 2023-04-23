using ProyectM2.SO;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class GameManager: MonoBehaviour
    {
        public static int _levelCurrency = 0;
        public static float _levelGas = 100;
        [SerializeField] Events _events;
        [SerializeField] GameObject _lose;

        private void OnEnable()
        {
            _events.SubscribeToEvent(GameOver);
        }
        private void OnDisable()
        {
            _events.UnsubscribeFromEvent(GameOver);
        }
        private void Start()
        {
            _levelCurrency = 0;
            _levelGas = 100;
        }

        public static void AddCurrency(int value)
        {
            _levelCurrency += value;
        }

        public static void AddGas(int value)
        {
            _levelGas += value;
        }
        public static void SubstractGas(float value)
        {
            _levelGas -= value;
        }
        public void QuitGame()
        {
            _levelCurrency = 0;
            _levelGas = 0;
        }
        public void GameOver()
        {
            Time.timeScale = 0f;
            _lose.SetActive(true);
        }
    }
}
