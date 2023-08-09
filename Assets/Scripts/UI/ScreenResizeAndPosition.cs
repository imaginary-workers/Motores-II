using UnityEngine;

namespace ProyectM2.UI
{
    public class ScreenResizeAndPosition : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float porcent;
        [SerializeField] private Transform _prueba;
        [SerializeField] private float _zPosition = 0;
        private void Awake()
        {
            var scale = Screen.height * porcent;
            transform.localScale = new Vector3(scale, scale, scale);
            var position = _prueba.transform.position;
            position.z = _zPosition;
            transform.position = position;
            var positionZ = transform.localPosition;
            positionZ.z = 0;
            transform.localPosition = positionZ;
        }
    }
}
