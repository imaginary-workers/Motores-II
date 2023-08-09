using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.Inputs
{
    public class ScreemButtonsStrategy : InputStrategy
    {
        public override StrategyType Type => StrategyType.Button;

        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _fireBackButton;
        [SerializeField] Button _buttonInteractableFireBack;
        private void Awake()
        {
            _buttonInteractableFireBack.interactable = false;
        }
        private void OnEnable()
        {
            EventManager.StartListening("OnFireBackButton", OnFireBackButton);
        }
        private void OnDisable()
        {
            EventManager.StopListening("OnFireBackButton", OnFireBackButton);

        }
        private void OnFireBackButton(object[] obj)
        {
            _buttonInteractableFireBack.interactable = (bool)obj[0];
        }
        public void Right()
        {
            Debug.Log(_inputManager);
            _inputManager.Horizontal(1);
        }

        public void Left()
        {
            Debug.Log(_inputManager);
            _inputManager.Horizontal(-1);
        }

        public void FireBack()
        {
            _inputManager.FireBack();
        }

        public override void Activate()
        {
            _leftButton?.gameObject.SetActive(true);
            _rightButton?.gameObject.SetActive(true);
            _fireBackButton?.gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            _leftButton?.gameObject.SetActive(false);
            _rightButton?.gameObject.SetActive(false);
            _fireBackButton?.gameObject.SetActive(false);
        }
    }
}