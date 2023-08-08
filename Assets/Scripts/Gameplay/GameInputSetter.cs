using ProyectM2.Inputs;
using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class GameInputSetter : MonoBehaviour
    {
        private ValuesToSaveInJson _myJsonData;

        private void OnEnable()
        {
            EventManager.StartListening("ChangeInputs", UpdateInput);
        }

        private void Start()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            SetInput(_myJsonData.input);
        }

        private void OnDisable()
        {
            EventManager.StopListening("ChangeInputs", UpdateInput);
        }

        private void UpdateInput(object[] obj)
        {
            InputType newInputType = (bool)obj[0] ? InputType.Tactil : InputType.ScreenButton;
            DataPersistance.Instance.UpdateInput(newInputType);
            SetInput(newInputType);
        }

        private void SetInput(InputType input)
        {
            InputManager.controlType = input;
        }

    }
}