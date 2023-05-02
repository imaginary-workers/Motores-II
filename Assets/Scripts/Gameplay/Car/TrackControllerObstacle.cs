using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class TrackControllerObstacle : TrackController
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //TODO llavar al evento gameover;
            }
        }
    }
}