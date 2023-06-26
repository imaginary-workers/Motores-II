using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class SectionsMainControllerUI : MonoBehaviour
    {
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