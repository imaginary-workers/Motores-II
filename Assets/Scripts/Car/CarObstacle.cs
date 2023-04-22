using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.Car
{
    public class CarObstacle : Car
    {
        [SerializeField] float _speed = 10;
        [SerializeField] AnimManager _myAnim;

        public override float Speed => _speed;

        public override AnimManager MyAnim => _myAnim;

        public override void MoveForward()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveLeft()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveRight()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
