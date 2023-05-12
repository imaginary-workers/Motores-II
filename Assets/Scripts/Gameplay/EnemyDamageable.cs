using ProyectM2.Car;
using ProyectM2.Gameplay.Car;
using UnityEngine;

namespace ProyectM2.Gameplay
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimationController animationController;
        [SerializeField] private TrackController _trackController;
        [SerializeField] private int _life = 3;

        [ContextMenu("Take Damage")]
        public void TakeDamage()
        {
            _life--;
            if (_life <= 0)
            {
                EventManager.TriggerEvent("EnemyDiedCutSceneStarted", _trackController.Track);
                animationController.DeathAnimation();
            }
            else
            {
                animationController.TurnLeftAnimation();
            }
        }
    }
}
