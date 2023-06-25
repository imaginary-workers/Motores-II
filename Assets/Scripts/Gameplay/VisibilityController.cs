using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Gameplay
{
    [RequireComponent(typeof(Renderer))]
    public class VisibilityController : MonoBehaviour
    {
        public UnityEvent OnStart;
        public UnityEvent OnObjectBecameVisible;
        private Renderer _renderer;

        public bool IsVisible => _renderer.isVisible;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            OnStart?.Invoke();
        }

        private void OnBecameVisible()
        {
            OnObjectBecameVisible?.Invoke();
        }
    }
}