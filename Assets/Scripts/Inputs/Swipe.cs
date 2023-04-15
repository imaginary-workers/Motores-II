using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Swipe : MonoBehaviour, IInput
    {

        [SerializeField] GameObject _player;
        Vector2 startTouchPosition;
        Vector2 endTouchPosition;

        public event Action<int> Horizontal;
        private void Update()
        {
            HorizontalSwipe();
        }

        public void HorizontalSwipe()
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                if(Math.Abs((endTouchPosition - startTouchPosition).x) < 100)return;

                if (endTouchPosition.x < startTouchPosition.x)
                {                   
                    Debug.Log("izquierda");

                    Horizontal?.Invoke(-1);
                }

                if (endTouchPosition.x > startTouchPosition.x)
                {
                    Debug.Log("derecha");
                    Horizontal?.Invoke(1);

                }
            }
        }
    }
}


