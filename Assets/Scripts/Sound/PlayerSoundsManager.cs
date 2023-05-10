using UnityEngine;

namespace ProyectM2.Sound
{
    public class PlayerSoundsManager: SoundsManager
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.StartListening("StartGameOver", OnEndGameOverHandler);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening("EndGameOver", OnEndGameOverHandler);
        }

        private void OnEndGameOverHandler(object[] obj)
        {
            Debug.Log("ENTROOOOOOOOOO");
            StopSound();
        }
    }
}