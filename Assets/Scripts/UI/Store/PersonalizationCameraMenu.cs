using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    [RequireComponent(typeof(Camera))]
    public class PersonalizationCameraMenu : MonoBehaviour
    {
        [Serializable]
        public class CameraConfiguration
        {
            public Transform positionAndRotation;
            [Range(1e-05f, 179f)] public float cameraFOV = 51.6f;
        }

        [SerializeField] private float _maxTimeToMoveCamera = 0.5f;
        [Header("Store Sections")]
        [SerializeField] private List<StoreSectionUI> _chassisSection;
        [SerializeField] private List<StoreSectionUI> _wheelsSection;
        [SerializeField] private List<StoreSectionUI> _glassSection;
        [SerializeField] private List<StoreSectionUI> _powerUpsSection;
        [SerializeField] private List<SectionsMainControllerUI> _mainSections;

        [Header("Camera Configs")]
        [SerializeField] private CameraConfiguration _chassisView;
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
            for (int i = 0; i < 2; i++)
            {
                _chassisSection[i].OnOpenMenu += GoToChassisView;
                _wheelsSection[i].OnOpenMenu += GoToWheelsView;
                _glassSection[i].OnOpenMenu += GoToGlassView;
                _powerUpsSection[i].OnOpenMenu += GoToPowerUpsView;
                _mainSections[i].OnGoToMainMenu += GoToMainMenuView;
            }
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
            for (int i = 0; i < 2; i++)
            {
                _chassisSection[i].OnOpenMenu -= GoToChassisView;
                _wheelsSection[i].OnOpenMenu -= GoToWheelsView;
                _glassSection[i].OnOpenMenu -= GoToGlassView;
                _powerUpsSection[i].OnOpenMenu -= GoToPowerUpsView;
                _mainSections[i].OnGoToMainMenu -= GoToMainMenuView;
            }
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
        
        private void GoToMainMenuView()
        {
            _nextCameraView = _originalMenuView;
            _currentTime = 0f;
        }
    }
}