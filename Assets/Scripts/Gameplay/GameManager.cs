using UnityEngine;
using ProyectM2.Persistence;
using ProyectM2.UI;
using System.Collections;
using ProyectM2.Gameplay.Car.Player;
using ProyectM2.Scenes;

namespace ProyectM2.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static int levelCurrency = 0;
        public static int currentLevel = 0;
        public static float levelGas = 100;
        private static float maxGas = 100;
        public static GameObject player;
        public static Vector3 positionInLevel = new(0, 0, 0);
        [SerializeField] private GameObject _lose;
        [SerializeField] private GameObject _won;
        [SerializeField] public static bool isInBonusLevel = false;
        [SerializeField] private PauseControllerUI _pauseController;
        bool _iWin = false;
        public static bool isInCutScene;
        public static bool isOnPause;

        private void OnEnable()
        {
            EventManager.StartListening("EndGameOver", GameOver);
            EventManager.StartListening("Won", OnWonHandler);
            EventManager.StartListening("EnemyDiedCutSceneStarted", OnWonHandler);
            EventManager.StartListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StartListening("TeleportToBonusLevel", TeleportToBonusLevel);
            EventManager.StartListening("TeleportReturnToLevel", ReturnFromBonusLevel);
            EventManager.StartListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StartListening("EnemyCutSceneEnded", OnEnemyCutSceneEnded);
        }


        private void OnDisable()
        {
            EventManager.StopListening("EndGameOver", GameOver);
            EventManager.StopListening("Won", OnWonHandler);
            EventManager.StopListening("EnemyDiedCutSceneStarted", OnWonHandler);
            EventManager.StopListening("TeleportToBonusLevel", TeleportToBonusLevel);
            EventManager.StopListening("TeleportReturnToLevel", ReturnFromBonusLevel);
            EventManager.StopListening("PlayerGetHit", OnPlayerGetHit);
            EventManager.StopListening("EnemyCutSceneStarted", OnEnemyCutSceneStarted);
            EventManager.StopListening("EnemyCutSceneEnded", OnEnemyCutSceneEnded);
        }

        private void OnEnemyCutSceneEnded(object[] obj)
        {
            isInCutScene = false;
        }

        private void OnEnemyCutSceneStarted(object[] obj)
        {
            isInCutScene = true;
        }

        private void Awake()
        {
            player = GameObject.FindObjectOfType<PlayerInputHorizontalMovement>().gameObject;
        }

        private void Start()
        {
            if (SessionGameData.GetData("IsInBonusLevel") != null)
            {
                isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");
            }

            if (!isInBonusLevel)
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
                    player.transform.root.forward = (Vector3)SessionGameData.GetData("ForwardOfPlayer");
                    var playerPathController = player.transform.root.GetComponent<PlayerPathController>();
                    playerPathController.SetCurrentPathTarget(playerPathController.GetClosestPathTarget());
                }

                if (SessionGameData.GetData("levelGas") != null)
                {
                    levelGas = (float)SessionGameData.GetData("levelGas");
                    EventManager.TriggerEvent("GasSubtract", levelGas);
                }
                else
                    levelGas = maxGas;
            }

            ScreenManager.Instance.Pause();
            _pauseController.StartCountingDownToStart();
        }

        public static void AddCurrency(int value)
        {
            if (isInBonusLevel)
            {
                levelCurrency += value * 2;
                EventManager.TriggerEvent("CurrencyModified", levelCurrency, value);
            }
            else
            {
                levelCurrency += value;
                EventManager.TriggerEvent("CurrencyModified", levelCurrency, value);
            }
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
            if (levelGas <= 0)
            {
                CutSceneManager.Instance.StartCutScene();
                EventManager.TriggerEvent("StartGameOver", Gameplay.GameOver.Gas);
            }
            else
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

        public void Won()
        {
            _iWin = true;
            SessionGameData.ResetData();
            _won.SetActive(true);
            EventManager.TriggerEvent("Won");
        }

        public void GameOver(object[] obj)
        {
            if (obj.Length <= 0) return;
            if ((GameOver)obj[0] == Gameplay.GameOver.Bonus)
            {
                SessionGameData.SaveData("IsInBonusLevel", !isInBonusLevel);
                SessionGameData.GetData("levelCurrency");
                SceneManager.Instance.ChangeScene(SceneManager.Instance.historyScene[^2]);
                return;
            }
            SessionGameData.ResetData();
            player.SetActive(false);
            _lose.SetActive(true);
        }

        private void OnWonHandler(object[] obj)
        {
            isInCutScene = true;
            StartCoroutine(WaitToWon(2f));
        }

        private IEnumerator WaitToWon(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            Won();
        }
    }
}