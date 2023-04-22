using System.Collections;
using SM = UnityEngine.SceneManagement.SceneManager;
using UnityEngine;

namespace ProyectM2.Managers
{
    public class SceneManager : Singleton<SceneManager>
    {
        [SerializeField] private LoadCanvasUI _loadCanvasUI;

        protected override void Awake()
        {
            base.Awake();
            _loadCanvasUI.DisplayLoadCanvas(false);
        }

        public void ChangeScene(string nextScene)
        {
            StartCoroutine(CO_ChangeScene(nextScene));
        }

        private IEnumerator CO_ChangeScene(string nextScene)
        {
            _loadCanvasUI.SetLoadTextTo("Loading");
            _loadCanvasUI.SetLoadBarTo(0f);
            _loadCanvasUI.DisplayLoadCanvas(true);
            yield return new WaitForSecondsRealtime(1f);
            var asyncLoad = SM.LoadSceneAsync(nextScene);
            asyncLoad.allowSceneActivation = false;
            while (asyncLoad.progress < 0.9f)
            {
                _loadCanvasUI.SetLoadBarTo(asyncLoad.progress);
                yield return null;
            }
            
            _loadCanvasUI.SetLoadBarTo(1f);
            _loadCanvasUI.SetLoadTextTo("Done");
            yield return new WaitForSecondsRealtime(1f);
            asyncLoad.allowSceneActivation = true;
            _loadCanvasUI.DisplayLoadCanvas(false);
        }
    }
}