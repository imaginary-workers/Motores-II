using UnityEngine;

namespace ProyectM2.Personalization
{
    public class SwipeRotationComponent : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 0.5f;
    
        [SerializeField]
        private Camera mainCamera;
    
        private bool firstTouchOnObject;
        private Vector2 initialTouchPosition;
    
        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }
    
        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
    
                if (touch.phase == TouchPhase.Began)
                {
                    // Check if the first touch is on this object
                    RaycastHit hit;
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        firstTouchOnObject = true;
                        initialTouchPosition = touch.position;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && firstTouchOnObject)
                {
                    // Calculate the swipe direction based on the difference between the current touch position and initial touch position
                    Vector2 swipeDirection = touch.position - initialTouchPosition;
    
                    // Rotate the object around the Y axis based on the swipe direction
                    transform.Rotate(0f, -swipeDirection.x * rotationSpeed, 0f);
                }
                else if (touch.phase == TouchPhase.Ended && firstTouchOnObject)
                {
                    firstTouchOnObject = false;
                }
            }
        }
    }
}