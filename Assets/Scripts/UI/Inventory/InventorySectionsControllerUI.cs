using System;
using System.Collections.Generic;
using ProyectM2.Personalization;
using UnityEngine;

namespace ProyectM2.UI.Inventory
{
    public class InventorySectionsControllerUI : MonoBehaviour
    {
        public event Action OnGoToMainMenu;
        [SerializeField, Tooltip("Poner el que se abrira primero de primero en la lista")]
        private List<InventorySectionUI> _sectionsUI;

        private void Awake()
        {
            foreach (var sectionUI in _sectionsUI)
            {
                sectionUI.Hide();
            }
            _sectionsUI[0].Show();
        }

        private void OnEnable()
        {
            OpenSection(_sectionsUI[0]);
        }

        private void OnDisable()
        {
            OnGoToMainMenu?.Invoke();
        }

        public void OpenSection(InventorySectionUI sectionController)
        {
            if (sectionController.IsVisible) return;

            foreach (var sectionUI in _sectionsUI)
            {
                sectionUI.Hide();
            }
            sectionController.Show();
        }
    }
}