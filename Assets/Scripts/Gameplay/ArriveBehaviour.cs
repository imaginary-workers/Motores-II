using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class ArriveBehaviour
    {
        private readonly ISpeedProvider _speedProvider;
        private readonly Transform _myTransform;

        public ArriveBehaviour(ISpeedProvider speedProvider, Transform myTransform)
        {
            _speedProvider = speedProvider;
            _myTransform = myTransform;
        }

        public void Arrive(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - _myTransform.position;
            float distance = direction.magnitude;

            if (distance > 0f)
            {
                var decelerationRate = _speedProvider.Speed / distance;
                var newSpeed = Mathf.Max(decelerationRate * Time.deltaTime, 0f);
                _speedProvider.Speed = newSpeed;
            }
        }
    }
}