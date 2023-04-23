using ProyectM2.Inputs;
using UnityEngine;

namespace ProyectM2.Gameplay.Car
{
    public class PlayerTrackController : TrackController
    {
        [SerializeField] float _substracttGas = 0.1f;

        private void Start()
        {
            Transform coche = transform.GetChild(0);

            if(coche != null)
            {
                coche.gameObject.tag = "Player";
            }
            
        }
        private void Update()
        {
            InputManager.CurrentInput.OnUpdate();
            GameManager.SubstractGas(_substracttGas * Time.deltaTime);
        }

        private void OnEnable()
        {
            InputManager.CurrentInput.Horizontal += OnHorizontal;
        }

        private void OnDisable()
        {
            InputManager.CurrentInput.Horizontal -= OnHorizontal;
        }


        private void OnHorizontal(int hor)
        {
            if (hor > 0)
                MoveRight();
            else
                MoveLeft();
        }
    }
}