using System;
using ProyectM2.Gameplay;
using UnityEngine;

namespace ProyectM2.Inputs
{
    public enum StrategyType
    {
        Button,
        Click,
        Both,
    }

    public abstract class InputStrategy : MonoBehaviour, IActivatable
    {
        protected InputManager _inputManager;
        protected bool _isActive = false;
        public abstract StrategyType Type { get; }
        public void SetManager(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public virtual void Activate()
        {
            _isActive = true;
        }

        public virtual void Deactivate()
        {
            _isActive = false;
        }
    }
}