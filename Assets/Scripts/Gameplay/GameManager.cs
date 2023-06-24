using UnityEngine;
using ProyectM2.Persistence;
using ProyectM2.UI;
using System.Collections;
using ProyectM2.Gameplay.Car.Player;
using ProyectM2.Scenes;
using ProyectM2.SO;

namespace ProyectM2.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static int currentLevel = 0;
        public static float levelGas = 100;
        private static float maxGas = 100;
        public static GameObject player;
        public static Vector3 positionInLevel = new(0, 0, 0);
        public static bool isInCutScene;
        public static bool isOnPause;
        public static bool isInBonusLevel = false;

        [SerializeField] private GameObject _lose;
        [SerializeField] private GameObject _won;
        [SerializeField] private PauseControllerUI _pauseController;
        [SerializeField] private DataIntObservable _levelCurrency;
        
        bool _iWin = false;

        private void Awake()
        {
            player = GameObject.FindObjectOfType<PlayerInputHorizontalMovement>().gameObject;
            ScreenManager.Instance.Pause();
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
                    var newCurrency = (int)SessionGameData.GetData("levelCurrency");

                    if (SessionGameData.GetData("CurrenciesOfBonusLevel") != null)
                    {
                        newCurrency += (int)SessionGameData.GetData("CurrenciesOfBonusLevel");
                    }

                    _levelCurrency.value = newCurrency;
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
            else
            {
                _levelCurrency.value = 0;
            }

            _pauseController.StartCountingDownToStart();
            Debug.Log(_levelCurrency.value);
        }

        private void OnEnable()
        {
            EventManager.StartListening("EndGameOver", GameOver);
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
            SessionGameData.SaveData("levelCurrency", _levelCurrency.value);
            SessionGameData.SaveData("levelGas", levelGas);
        }

        public void ReturnFromBonusLevel(object[] obj)
        {
            SessionGameData.SaveData("CurrenciesOfBonusLevel", _levelCurrency.value);
        }

        public void Retry()
        {
            _levelCurrency.value = 0;
            SessionGameData.ResetData();
            SceneManager.Instance.RestartLevel();
        }

        public void QuitGame()
        {
            if (!_iWin)
            {
                _levelCurrency.value = 0;
            }

            SessionGameData.ResetData();
            SceneManager.Instance.ChangeToMenuScene("MainMenu");
        }

        private void OnPlayerGetHit(object[] obj)
        {
            _levelCurrency.value -= 1;
        }

        [ContextMenu("Won")]
        public void Won()
        {
            _iWin = true;
            SessionGameData.ResetData();
            _won.SetActive(true);
            ScreenManager.Instance.Pause();
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
            ScreenManager.Instance.Pause();
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