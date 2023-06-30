using UnityEngine;

namespace ProyectM2.Gameplay.Car.Player
{
    public class PlayerFiresBackController_Tutorial : PlayerFiresBackController
    {
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (returnableBullet == null) return;
            EventManager.TriggerEvent("FirebackTutorial", true);
        }

        protected override void FirebackAction()
        {
            if (returnableBullet == null) return;
            EventManager.TriggerEvent("FirebackTutorial", false);
            base.FirebackAction();
        }
        protected override void OnTriggerExit(Collider other)
        {

            if (returnableBullet != null)
            {
                EventManager.TriggerEvent("FirebackTutorial", false);
                Time.timeScale = 1f;
                returnableBullet = null;
            }

        }
    }
}