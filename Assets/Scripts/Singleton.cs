using UnityEngine;

namespace ProyectM2
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                else
                {
                    Singleton<T>[] objects = FindObjectsOfType<Singleton<T>>();
                    if (objects.Length > 1)
                    {
                        for (int i = 0; i < objects.Length; i++)
                        {
                            if (objects[i] != _instance)
                            {
                                Destroy(objects[i].gameObject);
                            }
                        }
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}