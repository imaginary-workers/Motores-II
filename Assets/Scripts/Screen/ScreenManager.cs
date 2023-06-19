using System.Collections.Generic;

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

        public void Pop()
        {
            if (_stack.Count <= 1) return;

            _stack.Pop().Free();

            //
            // _stack.Push(newScreen);
            // newScreen.Activate();
        }
    }
}