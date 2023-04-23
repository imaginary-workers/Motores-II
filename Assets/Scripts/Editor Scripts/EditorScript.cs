using System.Collections.Generic;
using ProyectM2.Gameplay.Car.Path;
using UnityEngine;

namespace ProyectM2
{
    [ExecuteInEditMode]
    public class EditorScript : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjects;

        [ContextMenu("Set Path Targets")]
        public void SetPathTargets()
        {
            for (int i = 0; i < _gameObjects.Length; i++)
            {
                if (i < _gameObjects.Length - 1)
                {
                    _gameObjects[i].GetComponent<PathTargetInfo>().NextPathTarget = _gameObjects[i + 1];
                }
            }
        }

        [ContextMenu("Assing")]
        public void Assing()
        {
            var infos = GameObject.FindObjectsOfType<PathTargetInfo>();
            var go = new GameObject[infos.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                go[i] = infos[i].gameObject;
            }
            OrdenarPorNombre(go);
            _gameObjects = go;
        }
        private void OrdenarPorNombre(GameObject[] objetos)
        {
            ComparadorPorNombre comparador = new ComparadorPorNombre();
            System.Array.Sort(objetos, comparador);
        }

        private class ComparadorPorNombre : IComparer<GameObject>
        {
            public int Compare(GameObject x, GameObject y)
            {
                return x.name.CompareTo(y.name);
            }
        }
    }
}
