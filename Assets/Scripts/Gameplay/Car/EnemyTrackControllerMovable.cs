namespace ProyectM2.Gameplay.Car
{
    public class EnemyTrackControllerMovable: TrackControllerMovable
    {
        private void OnEnable()
        {
            EventManager.StartListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStartedHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyDiedCutSceneStarted", OnEnemyDiedCutSceneStartedHandler);
        }

        private void OnEnemyDiedCutSceneStartedHandler(object[] obj)
        {
            Destroy(this);
        }
    }
}