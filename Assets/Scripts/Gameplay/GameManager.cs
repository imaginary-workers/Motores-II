using ProyectM2.SO;
using UnityEngine;
using ProyectM2.Gameplay.Car;

namespace ProyectM2.Gameplay
{
    public class GameManager: MonoBehaviour
    {
        public static int levelCurrency = 0;
        public static float levelGas = 100;
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
            levelCurrency = 0;
            levelGas = 100;
        }

        public static void AddCurrency(int value)
        {
            levelCurrency += value;
            EventManager.TriggerEvent("CurrencyModified", levelCurrency, value);
        }

        public static void AddGas(int value)
        {
            levelGas += value;
            if(levelGas > 100)
            {
                levelGas = 100;
            }
            EventManager.TriggerEvent("GasModified",levelGas);
        }
        public static void SubstractGas(float value)
        {
            levelGas -= value;
        }
        public void QuitGame()
        {
            levelCurrency = 0;
            levelGas = 0;
        }
        public void GameOver()
        {
            Time.timeScale = 0f;
            _lose.SetActive(true);
        }
        private void Update()
        {
            Debug.Log("nafta de " + levelGas);
        }
    }
}

