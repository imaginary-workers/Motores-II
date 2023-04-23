using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ProyectM2.UI;
using SM = UnityEngine.SceneManagement.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void ChangeScene(Scene nextScene)
        {
            StartCoroutine(CO_ChangeScene(nextScene));
        }

        private IEnumerator CO_ChangeScene(Scene nextScene)
        {
            var sceneToLoad = new List<AsyncOperation>();
            _loadCanvasUI.SetLoadTextTo("Loading");
            _loadCanvasUI.SetLoadBarTo(0f);
            _loadCanvasUI.DisplayLoadCanvas(true);
            yield return new WaitForSecondsRealtime(1f);
            sceneToLoad.Add(SM.LoadSceneAsync(nextScene.name));
            if (nextScene.type == Scene.Type.Gameplay)
            {
                sceneToLoad.Add(SM.LoadSceneAsync(nextScene.type.ToString(), LoadSceneMode.Additive));
            }

            var progress = 0f;
            foreach (var operation in sceneToLoad)
            {
                while (!operation.isDone)
                {
                    progress += operation.progress;
                    _loadCanvasUI.SetLoadBarTo(progress/sceneToLoad.Count);
                    yield return null;
                }
            }
            _loadCanvasUI.SetLoadBarTo(1f);
            _loadCanvasUI.SetLoadTextTo("Done");
            yield return new WaitForSecondsRealtime(1f);
            _loadCanvasUI.DisplayLoadCanvas(false);
        }
    }
}