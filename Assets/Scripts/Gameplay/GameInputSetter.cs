using ProyectM2.Inputs;
using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class GameInputSetter : MonoBehaviour
    {
        private ValuesToSaveInJson _myJsonData;

        private void Start()
        {
            _myJsonData = DataPersistance.Instance.LoadGame();
            InputManager.controlType = _myJsonData.input;
        }

        public void UpdateInput()
        {
            DataPersistance.Instance.UpdateInput(InputType.Tactil);
        }
    }
}