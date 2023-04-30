using UnityEngine;

namespace ProyectM2
{
    public class ScreenResize : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float porcent;

        private void Awake()
        {
            var scale = Screen.height * porcent;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
