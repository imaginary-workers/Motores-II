using System;
using System.Collections.Generic;
using ProyectM2.Gameplay;
using ProyectM2.Managers;
using ProyectM2.Persistence;
using TMPro;
using UnityEngine;

namespace ProyectM2.UI
{
    public class MenuPrincipalUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private TextMeshProUGUI _timePlayedText;
        [SerializeField] private GameObject _currency;
        [SerializeField] private GameObject _menu1; 
        [SerializeField] private GameObject _levelsMenu; 
        [SerializeField] private GameObject _gameDataMenu; 
        [SerializeField] private GameObject _gameDataWarningPopUp; 
        [SerializeField] private GameObject _backButton;
        [SerializeField] private DataPersistance _myDataPersistance;
        ValuesToSaveInJson _myJsonData;

        private void Awake()
        {
            Time.timeScale = 1;
            //_myDataPersistance.SaveGame();
            _currency.SetActive(true);
            _menu1.SetActive(true);
            _levelsMenu.SetActive(false);
            _gameDataMenu.SetActive(false);
            _gameDataWarningPopUp.SetActive(false);
            _backButton.SetActive(false);
        }

        private void Start()
        {
            GetCurrencyData();
            
        }

        public void Play(int level)
        {
            GameManager.currentLevel = level;
            SceneManager.Instance.ChangeScene(new Scene("Level "+level, Scene.Type.Gameplay));
        }
#if UNITY_EDITOR
        [SerializeField] private string debuScene;
        [ContextMenu("Play Debug")]
        public void Play()
        {
            SceneManager.Instance.ChangeScene(new Scene(debuScene, Scene.Type.Gameplay));
        }
#endif
        public void GoToLevelsMenu()
        {
            ExecuteCommand(new ChangeMenuCommand(new []{_levelsMenu, _backButton}, new []{_menu1}));
        }

        public void GoToPersistence()
        {
            ExecuteCommand(new ChangeMenuCommand(new []{_gameDataMenu, _backButton}, new []{_menu1, _currency}));
        }
        
        public void GoToWarningDeleteSaveData()
        {
            ExecuteCommand(new ChangeMenuCommand(new []{_gameDataWarningPopUp}, new []{_gameDataMenu, _backButton}));
        }

        public void GoBack()
        {
            UndoLastCommand();
        }

        public void GetCurrencyData()
        {
            _myJsonData = _myDataPersistance.LoadGame();
            Debug.Log(_myJsonData);
            _currencyText.text = _myJsonData.totalCurrencyOfPlayer.ToString();
            var timeSpan = TimeSpan.FromSeconds(_myJsonData.timePlayed);
            _timePlayedText.text = timeSpan.ToString(@"hh\:mm\:ss");
        }

        private Stack<ICommand> commandStack = new Stack<ICommand>();

        private void ExecuteCommand(ICommand command) {
            command.Execute();
            commandStack.Push(command);
        }

        private void UndoLastCommand() {
            if (commandStack.Count > 0) {
                ICommand lastCommand = commandStack.Pop();
                lastCommand.Undo();
            }
        }
    }
    public interface ICommand {
        void Execute();
        void Undo();
    }
    public class ChangeMenuCommand : ICommand {
        private readonly GameObject[] _toShow;
        private readonly GameObject[] _toHide;

        public ChangeMenuCommand(GameObject[] toShow, GameObject[] toHide = null)
        {
            _toShow = toShow;
            _toHide = toHide;
        }

        public virtual void Execute() {
            foreach (var gameObject in _toHide)
            {
                gameObject.SetActive(false);
            }
            foreach (var gameObject in _toShow)
            {
                gameObject.SetActive(true);
            }
        }

        public virtual void Undo() {
            foreach (var gameObject in _toShow)
            {
                gameObject.SetActive(false);
            }
            foreach (var gameObject in _toHide)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
