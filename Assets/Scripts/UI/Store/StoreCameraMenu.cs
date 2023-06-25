using System;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    [RequireComponent(typeof(Camera))]
    public class StoreCameraMenu : MonoBehaviour
    {
        [Serializable]
        public class CameraConfiguration
        {
            public Transform positionAndRotation;
            [Range(1e-05f, 179f)] public float cameraFOV = 51.6f;
        }

        [SerializeField] private float _maxTimeToMoveCamera = 0.5f;

        [Header("Store Sections")] [SerializeField]
        private StoreSectionUI _chassisSection;

        [SerializeField] private StoreSectionUI _wheelsSection;
        [SerializeField] private StoreSectionUI _glassSection;
        [SerializeField] private StoreSectionUI _powerUpsSection;

        [Header("Camera Configs")] [SerializeField]
        private CameraConfiguration _chassisView;

        [SerializeField] private CameraConfiguration _glassView;
        [SerializeField] private CameraConfiguration _wheelView;
        [SerializeField] private CameraConfiguration _powerUpsView;
        [SerializeField] private CameraConfiguration _originalMenuView;
        private Camera _camera;
        private CameraConfiguration _nextCameraView;
        private CameraConfiguration _initialCameraView;
        private float _currentTime = 0;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _initialCameraView = _originalMenuView;
            transform.position = _originalMenuView.positionAndRotation.position;
            transform.forward = _originalMenuView.positionAndRotation.forward;
            _camera.fieldOfView = _originalMenuView.cameraFOV;
        }

        private void OnEnable()
        {
            _chassisSection.OnOpenMenu += GoToChassisView;
            _wheelsSection.OnOpenMenu += GoToWheelsView;
            _glassSection.OnOpenMenu += GoToGlassView;
            _powerUpsSection.OnOpenMenu += GoToPowerUpsView;
        }

        public void LateUpdate()
        {
            if (_nextCameraView == null) return;
            if (_nextCameraView == _initialCameraView) return;
            if (_currentTime > _maxTimeToMoveCamera) return;
            _currentTime += Time.deltaTime;
            float t = _currentTime / _maxTimeToMoveCamera;
            transform.position = Vector3.Lerp(_initialCameraView.positionAndRotation.position,
                _nextCameraView.positionAndRotation.position, t);
            transform.rotation = Quaternion.Slerp(_initialCameraView.positionAndRotation.rotation,
                _nextCameraView.positionAndRotation.rotation, t);
            _camera.fieldOfView = Mathf.Lerp(_initialCameraView.cameraFOV, _nextCameraView.cameraFOV, t);
            if (_currentTime >= _maxTimeToMoveCamera)
            {
                _currentTime = 0f;
                _initialCameraView = _nextCameraView;
                transform.position = _initialCameraView.positionAndRotation.position;
                transform.rotation = _initialCameraView.positionAndRotation.rotation;
                _camera.fieldOfView = _initialCameraView.cameraFOV;
            }
        }

        private void OnDisable()
        {
            _chassisSection.OnOpenMenu -= GoToChassisView;
            _wheelsSection.OnOpenMenu -= GoToWheelsView;
            _glassSection.OnOpenMenu -= GoToGlassView;
            _powerUpsSection.OnOpenMenu -= GoToPowerUpsView;
        }

        private void GoToPowerUpsView()
        {
            _nextCameraView = _powerUpsView;
            _currentTime = 0f;
        }

        private void GoToGlassView()
        {
            _nextCameraView = _glassView;
            _currentTime = 0f;
        }

        private void GoToWheelsView()
        {
            _nextCameraView = _wheelView;
            _currentTime = 0f;
        }

        private void GoToChassisView()
        {
            _nextCameraView = _chassisView;
            _currentTime = 0f;
        }
    }
}