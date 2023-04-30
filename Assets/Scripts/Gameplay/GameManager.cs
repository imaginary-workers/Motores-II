using ProyectM2.SO;
using UnityEngine;
using ProyectM2.Gameplay.Car;
using ProyectM2.Managers;
using ProyectM2.Persistence;

namespace ProyectM2.Gameplay
{
    public class GameManager: MonoBehaviour
    {
        public static int levelCurrency = 0;
        public static int currentLevel = 0;
        public static float levelGas = 100;
        public static GameObject player;
        public static Vector3 positionInLevel = new(0, 0, 0);
        [SerializeField] Events _events;
        [SerializeField] GameObject _lose;


        private void OnEnable()
        {
            _events.SubscribeToEvent(GameOver);
            EventManager.StartListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StartListening("TeleportToBonusLevel", SaveLastPositionInGame);
            EventManager.StartListening("TeleportReturnToLevel", SaveCurrenciesOfBonusLevel);
            EventManager.StartListening("OnPause", Pause);
        }

        private void OnDisable()
        {
            _events.UnsubscribeFromEvent(GameOver);
            EventManager.StopListening("TeleportToBonusLevel", SaveLastPositionInGame);
            EventManager.StopListening("TeleportReturnToLevel", SaveCurrenciesOfBonusLevel);
            EventManager.StopListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StopListening("OnPause", Pause);
        }

        private void Awake()
        {
            player = FindObjectOfType<PlayerTrackController>().gameObject;
        }
        private void Start()
        {
            levelCurrency = 0;
            levelGas = 100;

            if (SessionGameData.GetData("levelCurrency") != null)
            {
                levelCurrency = (int)SessionGameData.GetData("levelCurrency");
            }
            if (SessionGameData.GetData("LastPositionOfPlayer") != null)
            {
                player.transform.root.position = (Vector3)SessionGameData.GetData("LastPositionOfPlayer");
            }

            Debug.Log(levelCurrency);
        }

        public static void AddCurrency(int value)
        {
            levelCurrency += value;
            EventManager.TriggerEvent("CurrencyModified", levelCurrency, value);
        }
        
        public static void SubstractCurrency(int value)
        {
            levelCurrency -= value;
            if (levelCurrency < 0) levelCurrency = 0;
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

        public void SaveLastPositionInGame(object[] obj)
        {
            SessionGameData.SaveData("LastPositionOfPlayer", player.transform.root.position);
        }

        public void SaveCurrenciesOfBonusLevel(object[] obj)
        {
            SessionGameData.SaveData("CurrenciesOfBonusLevel", levelCurrency);
        }

        public void Retry()
        {
            SceneManager.Instance.RestartLevel();
        }

        public void QuitGame()
        {
            levelCurrency = 0;
            levelGas = 0;
            SceneManager.Instance.ChangeToMenuScene("MainMenu");
        }

        private void OnPlayerGetHit(object[] obj)
        {
            SubstractCurrency(1);
        }

        private void Pause(object[] obj)
        {
            if (obj.Length == 0) return;
            var isPause = (bool)obj[0];
            Time.timeScale = isPause ? 0 : 1;
        }

        [ContextMenu ("Won")]
        public void Won()
        {
            SceneManager.Instance.ChangeToMenuScene("MainMenu");
        }

        public void GameOver()
        {
            Time.timeScale = 0f;
            _lose.SetActive(true);
        }
    }
}

