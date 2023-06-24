using System;
using System.Collections.Generic;

namespace ProyectM2.Gameplay
{
    public class CutSceneManager : Singleton<CutSceneManager>
    {
        private class CutSceneSubscription
        {
            public string CutSceneName { get; }
            public CutSceneState State { get; }
            public Action Observer { get; }

            public CutSceneSubscription(string cutSceneName, CutSceneState state, Action observer)
            {
                CutSceneName = cutSceneName;
                State = state;
                Observer = observer;
            }
        }

        private List<CutSceneSubscription> subscriptions = new List<CutSceneSubscription>();
        public bool IsOnCutScene { get; private set; } = false;

        public void Subscribe(string cutSceneName, CutSceneState state, Action cutSceneObserver)
        {
            var subscription = new CutSceneSubscription(cutSceneName, state, cutSceneObserver);
            subscriptions.Add(subscription);
        }

        public void Unsubscribe(string cutSceneName, CutSceneState state, Action cutSceneObserver)
        {
            subscriptions.RemoveAll(s => s.CutSceneName == cutSceneName && s.State == state && s.Observer == cutSceneObserver);
        }

        public void StartCutScene(string cutSceneName)
        {
            IsOnCutScene = true;
            NotifyObservers(cutSceneName, CutSceneState.Started);
        }

        public void EndCutScene(string cutSceneName)
        {
            IsOnCutScene = false;
            NotifyObservers(cutSceneName, CutSceneState.Ended);
        }

        private void NotifyObservers(string cutSceneName, CutSceneState state)
        {
            foreach (var subscription in subscriptions)
            {
                if (subscription.CutSceneName == cutSceneName && subscription.State == state)
                {
                    subscription.Observer?.Invoke();
                }
            }
        }
    }
}