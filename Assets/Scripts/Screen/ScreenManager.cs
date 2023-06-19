using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Screen
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        private Stack<IScreen> _stack;

        protected override void Awake()
        {
            base.Awake();
            _stack = new Stack<IScreen>();
        }

        public void Push(IScreen newScreen)
        {
            if (_stack.Count > 0)
            {
                _stack.Peek().Deactivate();
            }

            _stack.Push(newScreen);
            newScreen.Activate();
        }

        public void Push(string resourceName)
        {
            var go = Instantiate(Resources.Load<GameObject>(resourceName));
            if (go.TryGetComponent(out IScreen newScreen))
            {
                Push(newScreen);
            }
        }
        public void Pop()
        {
            if (_stack.Count <= 1) return;

            _stack.Pop().Free();

            if (_stack.Count <= 0) return;
            
            _stack.Peek().Activate();
        }
    }
}