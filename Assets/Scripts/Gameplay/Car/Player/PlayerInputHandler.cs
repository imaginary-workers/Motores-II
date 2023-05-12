﻿using ProyectM2.Gameplay.Car.Controller;
using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerInputHandler: MonoBehaviour
    {
        [SerializeField] private TrackController _trackController;
        private void Update()
        {
            InputManager.CurrentInput.OnUpdate();
        }

        private void OnEnable()
        {
            InputManager.CurrentInput.Horizontal += OnHorizontal;
        }

        private void OnDisable()
        {
            InputManager.CurrentInput.Horizontal -= OnHorizontal;
        }
        
        private void OnHorizontal(int hor)
        {
            if (hor > 0)
                _trackController.MoveRight();
            else
                _trackController.MoveLeft();
        }
    }
}