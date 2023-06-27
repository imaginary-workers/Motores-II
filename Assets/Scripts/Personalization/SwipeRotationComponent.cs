using UnityEngine;

namespace ProyectM2.Personalization
{
    public class SwipeRotationComponent: MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 0.5f;
        [SerializeField] private Camera mainCamera;
        private bool firstTouchOnObject;
        private Vector2 previousTouchPosition;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit hit;
                    var ray = mainCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        firstTouchOnObject = true;
                        previousTouchPosition = touch.position;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && firstTouchOnObject)
                {
                    var swipeDirection = touch.position - previousTouchPosition;
                    transform.Rotate(0f, -swipeDirection.x * rotationSpeed, 0f);

                    previousTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended && firstTouchOnObject)
                {
                    firstTouchOnObject = false;
                }
            }
        }
    }
}