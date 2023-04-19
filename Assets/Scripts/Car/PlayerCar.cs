using ProyectM2.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace ProyectM2.Car
{
    public class PlayerCar : Car
    {
        [SerializeField] float _speed = 10;
        [SerializeField, Range(1, 5)] private float _horizontalRange = 3.22f;
        private int track = 0;
        [SerializeField] float _substracttGas = 0.1f;
        

        public override float Speed => _speed;

        private void Start()
        {
            Transform coche = transform.GetChild(0);

            if(coche != null)
            {
                coche.gameObject.tag = "Player";
            }
            
        }
        void Update()
        {
            InputManager.CurrentInput.OnUpdate();
            GameManager.SubstractGas(_substracttGas * Time.deltaTime);
        }
    
        public override void MoveRight()
        {
            if (track + 1 > 1) return;
            track++;
            MoveToTrack();
            myAnim.TurnRightAnimation();
        }

        private void MoveToTrack()
        {
            switch (track)
            {
                case -1:
                    Debug.Log("LEFT");
                    transform.localPosition += transform.localPosition + (Vector3.left * _horizontalRange);
                    break;
                case 1:
                    Debug.Log("Right");
                    transform.localPosition += transform.localPosition + (Vector3.right * _horizontalRange);
                    break;
                default:
                    transform.localPosition = new Vector3(0, 0, 0);
                    break;
            }
        }

        public override void MoveLeft()
        {
            Debug.Log("Left");
            if (track - 1 < -1) return;
            track--;
            MoveToTrack();
            myAnim.TurnLeftAnimation();
        }

        public override void MoveForward()
        {
        }
        
        void OnEnable()
        {
            InputManager.CurrentInput.Horizontal += OnHorizontal;
        }

        void OnDisable()
        {
            InputManager.CurrentInput.Horizontal -= OnHorizontal;
        }


        void OnHorizontal(int hor)
        {
            if (hor > 0)
                MoveRight();
            else
                MoveLeft();
        }
    }
}