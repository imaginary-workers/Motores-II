using System;
using System.Collections.Generic;
using ProyectM2.Gameplay.Car.Player;
using ProyectM2.Music;
using ProyectM2.Persistence;
using ProyectM2.Scenes;
using ProyectM2.Stamina;
using ProyectM2.UI.Commands;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class MenuPrincipalUI : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _timePlayedText;
        [SerializeField] private PlayerPersonalization _playerPersonalization;
        [SerializeField] private VolumeController _volumeController;
        [Header("Menus")] 
        [SerializeField] private GameObject _currency;
        [SerializeField] private GameObject _menu1;
        [SerializeField] private GameObject _levelsMenu;
        [SerializeField] private GameObject _gameDataMenu;
        [SerializeField] private GameObject _gameDataWarningPopUp;
        [SerializeField] private GameObject _controllerMenu;
        [SerializeField] private GameObject _storePanel;
        [SerializeField] private GameObject _itemStoreWindow;
        [SerializeField] private GameObject _header;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _popUpWindowStore;
        [SerializeField] private GameObject _warningExitGame;
        [SerializeField] private GameObject _buttonsTutorial;
        [SerializeField] private GameObject _swipeTutorial;
        [SerializeField] private Toggle _inputToggle;
        [SerializeField] private RectTransform _inputToggleHandle;

        private bool _toggleOn;
        ValuesToSaveInJson _myJsonData;
        private Stack<ICommand> commandStack = new Stack<ICommand>();

        [ContextMenu("Default Awake")]
        private void Awake()
        {
            _currency.SetActive(true);
            _menu1.SetActive(true);
            _levelsMenu.SetActive(false);
            _gameDataMenu.SetActive(false);
            _gameDataWarningPopUp.SetActive(false);
            _storePanel.SetActive(false);
            _itemStoreWindow.SetActive(false);
            _header.SetActive(true);
            _controllerMenu.SetActive(false);
            _inventoryPanel.SetActive(false);
            _warningExitGame.SetActive(false);           
        }

        private void OnEnable()
        {
            EventManager.StartListening("CurrencyModified", GetCurrencyData);
        }


        private void OnDisable()
        {
            EventManager.StartListening("CurrencyModified", GetCurrencyData);
        }

        private void Start()
        {
            UpdateCurrencyData();
            UpdateInputUI();
        }

        private void Update()
        {
            var timeSpan = TimeSpan.FromSeconds(TimePlayed.Instance.TotalTimePlayed);

            _timePlayedText.text = timeSpan.ToString(@"hh\:mm\:ss");
        }

        public void OnToggleSwitch()
        {
            var swipeHandlePosition = _inputToggleHandle.anchoredPosition;
            _inputToggleHandle.anchoredPosition = swipeHandlePosition*-1;
            _toggleOn = _inputToggle.isOn;
            _buttonsTutorial.SetActive(!_toggleOn);
            _swipeTutorial.SetActive(_toggleOn);
            EventManager.TriggerEvent("ChangeInputs", _toggleOn);
        }
        public void Play(int level)
        {
            if (StaminaSystem.Instance.HasEnoughStamina(1))
            {
                StaminaSystem.Instance.UseStamina(1);
                SceneManager.Instance.ChangeScene(new Scene("Level " + level, Scene.Type.Gameplay));
            }
        }

#if UNITY_EDITOR
        [SerializeField] private string debuScene;
        [ContextMenu("Play Debug")]
        public void Play()
        {
            SceneManager.Instance.ChangeScene(new Scene(debuScene, Scene.Type.Gameplay));
        }
#endif
        [ContextMenu("LevelsMenu")]
        public void GoToLevelsMenu()
        {
            ExecuteCommand(new ChangeMenuCommand(
                new[] { _levelsMenu }, new[] { _menu1 }));
        }

        [ContextMenu("Persistence")]
        public void GoToPersistence()
        {
            ExecuteCommand(new ChangeMenuCommand(new[] { _gameDataMenu }, new[] { _menu1 }));
        }

        [ContextMenu("WarningDeleteSaveData")]
        public void GoToWarningDeleteSaveData()
        {
            ExecuteCommand(new ChangeMenuCommand(new[] { _gameDataWarningPopUp }, new[] { _gameDataMenu, _header }));
        }

        [ContextMenu("WarningExitGame")]
        public void GoToWarningExitGame()
        {
            ExecuteCommand(new ChangeMenuCommand(new[] { _warningExitGame }, new[] { _menu1 }));
        }

        [ContextMenu("Controllers")]
        public void GoToControllers()
        {
            ExecuteCommand(new ChangeMenuCommand(new[] { _controllerMenu }, new[] { _menu1 }));
        }

        [ContextMenu("StoreMenu")]
        public void GoToStoreMenu()
        {
            ExecuteCommand(new ChangeInventoryStoreCommand(
                new[] { _storePanel }, new[] { _menu1 }, _playerPersonalization, _popUpWindowStore
                ));
        }

        [ContextMenu("StoreMenu")]
        public void GoToInventoryMenu()
        {
            ExecuteCommand(new ChangeInventoryStoreCommand(
                new[] { _inventoryPanel }, new[] { _menu1 }, _playerPersonalization
            ));
        }

        [ContextMenu("GoBack")]
        public void GoBack()
        {
            if (_menu1.activeSelf)
                GoToWarningExitGame();
            else
                UndoLastCommand();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        private void UpdateInputUI()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            Debug.Log($"Update Input UI load json {_myJsonData.input}");
            if (_myJsonData.input == Inputs.InputType.Tactil)
                _toggleOn = true;
            else
                _toggleOn = false;

            _buttonsTutorial.SetActive(!_toggleOn);
            _swipeTutorial.SetActive(_toggleOn);
            _inputToggle.isOn = _toggleOn;
        }

        public void UpdateCurrencyData()
        {
            DataPersistance.Instance.UpdateCurrency();
            _myJsonData = DataPersistance.Instance.LoadGame();
            _currencyText.text = _myJsonData.totalCurrencyOfPlayer.ToString();
        }

        public void GetCurrencyData(object[] obj)
        {
            UpdateCurrencyData();
        }

        public void DeteleData()
        {
            TimePlayed.Instance.ResetTime();
            _volumeController.ResetVolume();
            DataPersistance.Instance.DeleteData();
            UpdateCurrencyData();
        }

        private void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandStack.Push(command);
        }

        private void UndoLastCommand()
        {
            if (commandStack.Count > 0)
            {
                ICommand lastCommand = commandStack.Pop();
                lastCommand.Undo();
            }
        }
    }
}