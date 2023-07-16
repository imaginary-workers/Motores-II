using ProyectM2.Gameplay.Car.Controller;
using UnityEngine;

namespace ProyectM2.Car.Controller
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _myAnim;
        [SerializeField] private TrackController _trackController;
        private int turnLeftParameterId;
        private int turnRightParameterId;
        private int crashParameterId;
        private int hipParameterId;
        private int jumpParameterId;
        private int leftLight;
        private int rightLight;
        private int damaged;

        private void Start()
        {
            turnRightParameterId = Animator.StringToHash("TurnRight");
            turnLeftParameterId = Animator.StringToHash("TurnLeft");
            crashParameterId = Animator.StringToHash("Crash");
            hipParameterId = Animator.StringToHash("Hip");
            jumpParameterId = Animator.StringToHash("Jump");
            leftLight = Animator.StringToHash("LightLeft");
            rightLight = Animator.StringToHash("LightRight");
            damaged = Animator.StringToHash("Damaged");
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

        public void LeftLightAnimation()
        {
            _myAnim.SetTrigger(leftLight);
        }
        public void RightLightAnimation()
        {
            _myAnim.SetTrigger(rightLight);           
        }
        public void DamagedPlayerAnimation()
        {
            _myAnim.SetTrigger(damaged);
        }
    }
}
