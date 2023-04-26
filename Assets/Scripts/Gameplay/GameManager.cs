using ProyectM2.SO;
using UnityEngine;
using ProyectM2.Gameplay.Car;

namespace ProyectM2.Gameplay
{
    public class GameManager: MonoBehaviour
    {
        public static int _levelCurrency = 0;
        public static float _levelGas = 100;
        public static GameObject player;
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

        private void Awake()
        {
            player = FindObjectOfType<PlayerTrackController>().gameObject;
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

