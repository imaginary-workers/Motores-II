using UnityEngine;

namespace ProyectM2.UI
{
    public class ScreenResizeAndPosition : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float porcent;
        [SerializeField] private Transform _prueba;
        private void Awake()
        {
            var scale = Screen.height * porcent;
            transform.localScale = new Vector3(scale, scale, scale);
            transform.position = _prueba.transform.position;
        }
    }
}
