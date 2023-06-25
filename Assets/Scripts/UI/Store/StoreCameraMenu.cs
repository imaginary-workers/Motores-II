using System;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class StoreCameraMenu : MonoBehaviour
    {
        [Serializable]
        public class CameraConfiguration
        {
            public Transform _positionAndRotation;
            [Range(1e-05f, 179f)] public float _cameraFOV = 51.6f;
        }

        [SerializeField] private StoreSectionUI _chassisSection;
        [SerializeField] private StoreSectionUI _wheelsSection;
        [SerializeField] private StoreSectionUI _glassSection;
        [SerializeField] private StoreSectionUI _powerUpsSection;
        [SerializeField] private CameraConfiguration _chassisView;
        [SerializeField] private CameraConfiguration _glassView;
        [SerializeField] private CameraConfiguration _wheelView;
        [SerializeField] private CameraConfiguration _powerUpsView;
        [SerializeField] private CameraConfiguration _originalView;
        [SerializeField] private float _maxTimeToMoveCamera = 0.5f;
        private CameraConfiguration _nextView;
        private CameraConfiguration _initialView;
        private float _currentTime = -1f;

        private void OnEnable()
        {
            _chassisSection.OnOpenMenu += GoToChassisView;
            _wheelsSection.OnOpenMenu += GoToWheelsView;
            _glassSection.OnOpenMenu += GoToGlassView;
            _powerUpsSection.OnOpenMenu += GoToPowerUpsView;
            _initialView = _originalView;
        }

        public void LateUpdate()
        {
            if (_nextView == _initialView) return;
            if (_currentTime >= 0f && _currentTime <= _maxTimeToMoveCamera)
            {
                _currentTime += Time.deltaTime;
                float t = _currentTime / _maxTimeToMoveCamera;
                transform.position = Vector3.Lerp(_initialView, _nextView, t);
                if (_currentTime >= _maxTimeToMoveCamera)
                {
                    _currentTime = 0f;
                    _initialView = _nextView;
                }
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
            _nextView = _powerUpsView;
            _currentTime = 0f;
        }

        private void GoToGlassView()
        {
            _nextView = _glassView;
            _currentTime = 0f;
        }

        private void GoToWheelsView()
        {
            _nextView = _wheelView;
            _currentTime = 0f;
        }

        private void GoToChassisView()
        {
            _nextView = _chassisView;
            _currentTime = 0f;
        }
    }
}