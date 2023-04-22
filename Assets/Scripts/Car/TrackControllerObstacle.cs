using UnityEngine;

namespace ProyectM2.Car
{
    public class TrackControllerObstacle : TrackController
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //TODO llavar al evento gameover;
                Debug.Log("<br><color=green>TODO</color></br> llavar al evento gameover;");
            }
        }
    }
}