using System.Collections;
using System.Collections.Generic;
using ProyectM2.UI;
using SM = UnityEngine.SceneManagement.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProyectM2.Managers
{
    public class SceneManager : Singleton<SceneManager>
    {
        [SerializeField] private LoadCanvasUI _loadCanvasUI;
        public List<Scene> historyScene = new List<Scene>();

        protected override void Awake()
        {
            base.Awake();
            _loadCanvasUI.DisplayLoadCanvas(false);
        }

        public void ChangeToMenuScene(string scene)
        {
            ChangeScene(new Scene(scene, Scene.Type.Menu));
        }

        public void ChangeScene(Scene nextScene)
        {
            StartCoroutine(CO_ChangeScene(nextScene));
        }
        public void RestartLevel()
        {
            var currentScene = SM.GetActiveScene();
            StartCoroutine(CO_ChangeScene(new Scene(currentScene.name, Scene.Type.Gameplay), true));

        }
        private IEnumerator CO_ChangeScene(Scene nextScene, bool restart = false)
        {
            var sceneToLoad = new List<AsyncOperation>();
            _loadCanvasUI.SetLoadTextTo("Cargando");
            _loadCanvasUI.SetLoadBarTo(0f);
            _loadCanvasUI.DisplayLoadCanvas(true);
            yield return new WaitForSecondsRealtime(1f);
            if (restart)
            {
                SM.UnloadSceneAsync(nextScene.name);
                SM.UnloadSceneAsync(Scene.Type.Gameplay.ToString());
            }
            sceneToLoad.Add(SM.LoadSceneAsync(nextScene.name));
            if (nextScene.type == Scene.Type.Gameplay)
            {
                sceneToLoad.Add(SM.LoadSceneAsync(nextScene.type.ToString(), LoadSceneMode.Additive));
                historyScene.Add(nextScene);
            }
            else
            {
                historyScene.Clear();
            }

            var progress = 0f;
            foreach (var operation in sceneToLoad)
            {
                while (!operation.isDone)
                {
                    progress += operation.progress;
                    _loadCanvasUI.SetLoadBarTo(progress / sceneToLoad.Count);
                    yield return null;
                }
            }
            _loadCanvasUI.SetLoadBarTo(1f);
            _loadCanvasUI.SetLoadTextTo("Listo");
            yield return new WaitForSecondsRealtime(1f);
            _loadCanvasUI.DisplayLoadCanvas(false);
        }
    }
}