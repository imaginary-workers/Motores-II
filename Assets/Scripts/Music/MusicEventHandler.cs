
namespace ProyectM2.Music
{
    public class MusicEventHandler: Singleton<MusicEventHandler>
    {
        private void OnEnable()
        {
            EventManager.StartListening("OnChangeScene", OnChangeSceneHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnChangeScene", OnChangeSceneHandler);
        }

        private void OnChangeSceneHandler(object[] obj)
        {
            MusicManager.Instance.StopMusic();
        }
    }
}