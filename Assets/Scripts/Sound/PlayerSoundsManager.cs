namespace ProyectM2.Sound
{
    public class PlayerSoundsManager: SoundsManager
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.StopListening("StartGameOver", OnEndGameOverHandler);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening("EndGameOver", OnEndGameOverHandler);
        }

        private void OnEndGameOverHandler(object[] obj)
        {
            StopSound();
        }
    }
}