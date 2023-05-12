using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Car.Controller
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] Animator _myAnim;
        [SerializeField] TrackController _trackController;
        int turnLeftParameterId;
        int turnRightParameterId;
        int crashParameterId;
        int hipParameterId;
        int jumpParameterId;

        private void Start()
        {
            turnRightParameterId = Animator.StringToHash("TurnRight");
            turnLeftParameterId = Animator.StringToHash("TurnLeft");
            crashParameterId = Animator.StringToHash("Crash");
            hipParameterId = Animator.StringToHash("Hip");
            jumpParameterId = Animator.StringToHash("Jump");
        }

        private void OnEnable()
        {
            _trackController.Suscribe(TurnHandler);
        }

        private void OnDisable()
        {
            _trackController.Unsuscribe(TurnHandler);
        }

        private void TurnHandler(string dir)
        {
            if (dir == "Right") TurnRightAnimation();
            else TurnLeftAnimation();
        }

        public void TurnLeftAnimation()
        {
            _myAnim.SetTrigger(turnLeftParameterId);
        }

        public void HipUpAnimation()
        {
            _myAnim.SetTrigger(hipParameterId);
        }

        public void TurnRightAnimation()
        {
            _myAnim.SetTrigger(turnRightParameterId);
        }

        public void DeathAnimation()
        {
            _myAnim.SetTrigger(crashParameterId);
            enabled = false;
        }

        public void JumpAnimation()
        {
            _myAnim.SetTrigger(jumpParameterId);
        }
    }
}
