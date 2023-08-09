using UnityEngine;
using UnityEngine.Events;

namespace ProyectM2.Gameplay
{
    public class VisibilityController : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        public UnityEvent OnStart;
        public UnityEvent OnObjectBecameVisible;

        public bool IsVisible => _renderer.isVisible;

        private void LateUpdate()
        {
            if (!IsVisible) return;
            OnObjectBecameVisible?.Invoke();
            Destroy(this);
        }

        private void Start()
        {
            OnStart?.Invoke();
        }
    }
}