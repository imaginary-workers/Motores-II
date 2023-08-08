using ProyectM2.Inputs;
using ProyectM2.Persistence;
using System;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class GameInputSetter : Singleton<GameInputSetter>
    {
        private ValuesToSaveInJson _myJsonData;

        private void OnEnable()
        {
            EventManager.StartListening("ChangeInputs", UpdateInput);
        }

        private void Start()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            InputManager.controlType = _myJsonData.input;
        }

        private void OnDisable()
        {
            EventManager.StopListening("ChangeInputs", UpdateInput);
        }

        private void UpdateInput(object[] obj)
        {
            Debug.Log($"Update Input {(bool)obj[0]}");
            if ((bool)obj[0])
                DataPersistance.Instance.UpdateInput(InputType.Tactil);
            else
                DataPersistance.Instance.UpdateInput(InputType.ScreenButton);
        }

    }
}