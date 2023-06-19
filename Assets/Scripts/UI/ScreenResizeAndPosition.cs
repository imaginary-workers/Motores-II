using UnityEngine;

namespace ProyectM2.UI
{
    public class ScreenResizeAndPosition : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float porcent;
        [SerializeField] private Transform _prueba;
        [SerializeField] private float _zPosition;
        private void Awake()
        {
            var scale = UnityEngine.Screen.height * porcent;
            transform.localScale = new Vector3(scale, scale, scale);
            var position = _prueba.transform.position;
            position.z = _zPosition;
            transform.position = position;
        }
    }
}
