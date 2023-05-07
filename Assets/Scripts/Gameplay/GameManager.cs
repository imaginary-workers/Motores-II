using ProyectM2.SO;
using UnityEngine;
using ProyectM2.Gameplay.Car;
using ProyectM2.Managers;
using ProyectM2.Persistence;
using ProyectM2.UI;
using System;
using System.Collections;
using ProyectM2.Gameplay.Car.Path;

namespace ProyectM2.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static int levelCurrency = 0;
        public static int currentLevel = 0;
        public static float levelGas = 100;
        public static GameObject player;
        public static Vector3 positionInLevel = new(0, 0, 0);
        [SerializeField] private GameObject _lose;
        [SerializeField] private GameObject _won;
        [SerializeField] public static bool _isInBonusLevel = false;
        [SerializeField] private PauseControllerUI _pauseController;
        bool _iWin = false;

        private void OnEnable()
        {
            EventManager.StartListening("GameOver", GameOver);
            EventManager.StartListening("Won", OnWonHandler);
            EventManager.StartListening("GameOverBonusLevel", BonusGameOver);
            EventManager.StartListening("EnemyDiedCutSceneStarted", OnWonHandler);
            EventManager.StartListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StartListening("TeleportToBonusLevel", TeleportToBonusLevel);
            EventManager.StartListening("TeleportReturnToLevel", ReturnFromBonusLevel);
            EventManager.StartListening("OnPause", Pause);
        }


        private void OnDisable()
        {
            EventManager.StopListening("GameOver", GameOver);
            EventManager.StopListening("Won", OnWonHandler);
            EventManager.StopListening("GameOverBonusLevel",BonusGameOver);
            EventManager.StopListening("EnemyDiedCutSceneStarted", OnWonHandler);
            EventManager.StopListening("TeleportToBonusLevel", TeleportToBonusLevel);
            EventManager.StopListening("TeleportReturnToLevel", ReturnFromBonusLevel);
            EventManager.StopListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StopListening("OnPause", Pause);
        }

        private void Awake()
        {
            player = FindObjectOfType<PlayerTrackController>().gameObject;
            Debug.Log(player.name);
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

                    EventManager.TriggerEvent("CurrencyModified", levelCurrency);
                }

                if (SessionGameData.GetData("LastPositionOfPlayer") != null)
                {
                    player.transform.root.position = (Vector3)SessionGameData.GetData("LastPositionOfPlayer");
                    var playerPathManager = player.transform.root.GetComponent<PlayerPathManager>();
                    playerPathManager.SetCurrentPathTarget(playerPathManager.GetClosestPathTarget());
                    player.transform.root.forward = (Vector3)SessionGameData.GetData("ForwardOfPlayer");
                }

                if (SessionGameData.GetData("levelGas") != null)
                {
                    levelGas = (float)SessionGameData.GetData("levelGas");
                    EventManager.TriggerEvent("GasSubtract", levelGas);
                }
            }

            _pauseController.StartCountingDownToStart();
            Time.timeScale = 0;
        }

        private void Update()
        {
            Debug.Log(player.name);
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
            if (levelGas > 100)
            {
                levelGas = 100;
            }

            EventManager.TriggerEvent("GasModified", levelGas);
        }

        public static void SubstractGas(float value)
        {
            levelGas -= value;
            EventManager.TriggerEvent("GasSubtract", levelGas);
        }

        public void TeleportToBonusLevel(object[] obj)
        {
            SessionGameData.SaveData("LastPositionOfPlayer", player.transform.root.position);
            SessionGameData.SaveData("ForwardOfPlayer", player.transform.root.forward);
            SessionGameData.SaveData("levelCurrency", levelCurrency);
            SessionGameData.SaveData("levelGas", levelGas);
        }

        public void ReturnFromBonusLevel(object[] obj)
        {
            SessionGameData.SaveData("CurrenciesOfBonusLevel", levelCurrency);
        }

        public void Retry()
        {
            SubstractCurrency(levelCurrency);
            SessionGameData.ResetData();
            SceneManager.Instance.RestartLevel();
        }

        public void QuitGame()
        {
            if (!_iWin)
            {
                SubstractCurrency(levelCurrency);
            }

            SessionGameData.ResetData();
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
            PauseGame(isPause);
        }

        private void PauseGame(bool isPause)
        {
            Time.timeScale = isPause ? 0 : 1;
        }

        public void Won()
        {
            _iWin = true;
            SessionGameData.ResetData();
            _won.SetActive(true);
            PauseGame(true);
        }

        public void GameOver(object[] obj)
        {
            SessionGameData.ResetData();
            Time.timeScale = 0f;
            _lose.SetActive(true);
        }

        private void BonusGameOver(object[] obj)
        {
            SessionGameData.SaveData("IsInBonusLevel", !_isInBonusLevel);
            SessionGameData.GetData("levelCurrency");
            SceneManager.Instance.ChangeScene(SceneManager.Instance.historyScene[^2]);
        }

        private void OnWonHandler(object[] obj)
        {
            StartCoroutine(WaitToWon(2f));
        }

        private IEnumerator WaitToWon(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            Won();
        }
    }
}