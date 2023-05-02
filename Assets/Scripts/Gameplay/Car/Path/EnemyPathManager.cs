namespace ProyectM2.Gameplay.Car.Path
{
    public class EnemyPathManager: PathManager
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