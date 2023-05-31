using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Gameplay
{
    public class VisibilityEvents : MonoBehaviour
    {
        public UnityEvent OnStart;
        public UnityEvent OnObjectBecameVisible;

        private void OnBecameVisible()
        {
            Debug.Log("Debio RENAUDAR");
            OnObjectBecameVisible?.Invoke();
        }

        private void Start()
        {
            Debug.Log("Debio DETENER");
            OnStart?.Invoke();
        }
    }
}