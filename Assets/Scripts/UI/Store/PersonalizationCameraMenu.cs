using System;
using ProyectM2.Personalization;
using ProyectM2.UI.Inventory;
using ProyectM2.UI.Sections;
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
        [SerializeField] private StoreSectionUI store_chassisSection;
        [SerializeField] private StoreSectionUI store_wheelsSection;
        [SerializeField] private StoreSectionUI store_glassSection;
        [SerializeField] private StoreSectionUI store_powerUpsSection;
        [SerializeField] private StoreSectionsControllerUI _storeSectionsController;

        [Header("Personalization Sections")]
        [SerializeField] private InventorySectionUI inve_chassisSection;
        [SerializeField] private InventorySectionUI inve_wheelsSection;
        [SerializeField] private InventorySectionUI inve_glassSection;
        [SerializeField] private InventorySectionUI inve_powerUpsSection;
        [SerializeField] private InventorySectionsControllerUI _inventorySectionsController;

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
            store_chassisSection.OnOpenMenu += GoToChassisView;
            store_wheelsSection.OnOpenMenu += GoToWheelsView;
            store_glassSection.OnOpenMenu += GoToGlassView;
            store_powerUpsSection.OnOpenMenu += GoToPowerUpsView;
            _storeSectionsController.OnGoToMainMenu += GoToStoreMenuView;
            inve_chassisSection.OnOpenMenu += GoToChassisView;
            inve_wheelsSection.OnOpenMenu += GoToWheelsView;
            inve_glassSection.OnOpenMenu += GoToGlassView;
            inve_powerUpsSection.OnOpenMenu += GoToPowerUpsView;
            _inventorySectionsController.OnGoToMainMenu += GoToStoreMenuView;
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
            store_chassisSection.OnOpenMenu -= GoToChassisView;
            store_wheelsSection.OnOpenMenu -= GoToWheelsView;
            store_glassSection.OnOpenMenu -= GoToGlassView;
            store_powerUpsSection.OnOpenMenu -= GoToPowerUpsView;
            _storeSectionsController.OnGoToMainMenu -= GoToStoreMenuView;
            inve_chassisSection.OnOpenMenu -= GoToChassisView;
            inve_wheelsSection.OnOpenMenu -= GoToWheelsView;
            inve_glassSection.OnOpenMenu -= GoToGlassView;
            inve_powerUpsSection.OnOpenMenu -= GoToPowerUpsView;
            _inventorySectionsController.OnGoToMainMenu -= GoToStoreMenuView;
        }

        private void GoToPowerUpsView()
        {
            Debug.Log("GoToPowerUpsView");
            _nextCameraView = _powerUpsView;
            _currentTime = 0f;
        }

        private void GoToGlassView()
        {
            Debug.Log("GoToGlassView");
            _nextCameraView = _glassView;
            _currentTime = 0f;
        }

        private void GoToWheelsView()
        {
            Debug.Log("GoToWheelsView");
            _nextCameraView = _wheelView;
            _currentTime = 0f;
        }

        private void GoToChassisView()
        {
            Debug.Log("GoToChassisView");
            _nextCameraView = _chassisView;
            _currentTime = 0f;
        }

        private void GoToStoreMenuView()
        {
            Debug.Log("GoToMainMenuView");
            _nextCameraView = _originalMenuView;
            _currentTime = 0f;
        }
    }
}