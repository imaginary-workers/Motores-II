using System;
using System.Collections.Generic;

namespace ProyectM2.Gameplay
{
    public class ScreenManager: Singleton<ScreenManager>
    {
        private event Action Activated;
        private event Action Deactivated;

        public void Subscribe(IActivatable activatable)
        {
            Activated += activatable.Activate;
            Deactivated += activatable.Deactivate;
        }

        public void Unsubscribe(IActivatable activatable)
        {
            Activated -= activatable.Activate;
            Deactivated -= activatable.Deactivate;
        }

        public void Pause()
        {
            Deactivated?.Invoke();
        }

        public void Resume()
        {
            Activated?.Invoke();
        }
    }
}