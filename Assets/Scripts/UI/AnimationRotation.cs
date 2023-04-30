using UnityEngine;

namespace ProyectM2.UI
{
    public class AnimationRotation : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.Rotate(Vector3.up, 80 * Time.deltaTime);
        }
    }
}
