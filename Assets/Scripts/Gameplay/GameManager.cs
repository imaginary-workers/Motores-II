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
        [SerializeField] bool _isInBonusLevel = false;



        private void OnEnable()
        {
            _events.SubscribeToEvent(GameOver);
            EventManager.StartListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StartListening("TeleportToBonusLevel", TeleportToBonusLevel);
            EventManager.StartListening("TeleportReturnToLevel", ReturnFromBonusLevel); ;
            EventManager.StartListening("OnPause", Pause);
        }

        private void OnDisable()
        {
            _events.UnsubscribeFromEvent(GameOver);
            EventManager.StopListening("TeleportToBonusLevel", TeleportToBonusLevel);
            EventManager.StopListening("TeleportReturnToLevel", ReturnFromBonusLevel);
            EventManager.StopListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StopListening("OnPause", Pause);
        }

        private void Awake()
        {
            player = FindObjectOfType<PlayerTrackController>().gameObject;
        }
        private void Start()
        {

            if (SessionGameData.GetData("IsInBonusLevel") != null)
            {
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");
            }

            if (!_isInBonusLevel)
            {
                if (SessionGameData.GetData("levelCurrency") != null)
                {
                    levelCurrency = (int)SessionGameData.GetData("levelCurrency");
                    if (SessionGameData.GetData("CurrenciesOfBonusLevel") != null)
                    {
                        levelCurrency += (int)SessionGameData.GetData("CurrenciesOfBonusLevel");
                    }
                    Debug.Log(levelCurrency);
                    EventManager.TriggerEvent("CurrencyModified", levelCurrency);

                }
                if (SessionGameData.GetData("LastPositionOfPlayer") != null)
                {
                    player.transform.root.position = (Vector3)SessionGameData.GetData("LastPositionOfPlayer");
                }
                if (SessionGameData.GetData("CurrenciesOfBonusLevel") != null)
                {
                    levelCurrency += (int)SessionGameData.GetData("CurrenciesOfBonusLevel");
                }
            }

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

        public void TeleportToBonusLevel(object[] obj)
        {
            Debug.Log(levelCurrency);
            SessionGameData.SaveData("LastPositionOfPlayer", player.transform.root.position);
            SessionGameData.SaveData("levelCurrency", levelCurrency);
        }

        public void ReturnFromBonusLevel(object[] obj)
        {
            Debug.Log(levelCurrency);
            SessionGameData.SaveData("CurrenciesOfBonusLevel", levelCurrency);
        }

        public void Retry()
        {
            SessionGameData.ResetData();
            SceneManager.Instance.RestartLevel();
        }

        public void QuitGame()
        {
            SessionGameData.ResetData();
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
            SessionGameData.ResetData();
            SceneManager.Instance.ChangeToMenuScene("MainMenu");
        }

        public void GameOver()
        {
            SessionGameData.ResetData();
            Time.timeScale = 0f;
            _lose.SetActive(true);
        }
    }
}

