using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class ObjectPool
    {
        private List<GameObject> objects;
        private GameObject prefab;
        private Transform parentTransform;

        public ObjectPool(GameObject prefab, int initialPoolSize, Transform parentTransform = null)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            objects = new List<GameObject>(initialPoolSize);
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewObject();
            }
        }

        public GameObject GetObject()
        {
            GameObject obj = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (!objects[i].activeInHierarchy)
                {
                    obj = objects[i];
                    obj.SetActive(true);
                    break;
                }
            }
            if (obj == null)
            {
                obj = CreateNewObject();
                obj.SetActive(true);
            }
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(parentTransform);
        }

        private GameObject CreateNewObject()
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.SetParent(parentTransform);
            objects.Add(obj);
            return obj;
        }
    }
}