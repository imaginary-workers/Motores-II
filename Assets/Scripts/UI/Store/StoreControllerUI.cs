using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class StoreControllerUI : MonoBehaviour
    {
        [SerializeField] private List<StoreSectionUI> _sectionsUI;

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