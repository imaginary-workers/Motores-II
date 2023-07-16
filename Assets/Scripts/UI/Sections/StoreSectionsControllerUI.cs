using System;
using System.Collections.Generic;
using ProyectM2.UI.Store;
using UnityEngine;

namespace ProyectM2.UI.Sections
{
    public class StoreSectionsControllerUI : MonoBehaviour
    {
        public event Action OnGoToMainMenu;
        [SerializeField, Tooltip("Poner el que se abrira primero de primero en la lista")]
        private List<StoreSectionUI> _sectionsUI;

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

        public void OpenSection(StoreSectionUI sectionController)
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