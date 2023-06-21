using TMPro;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class StoreSectionUI: MonoBehaviour
    {
        private string _sectionName = "";
        [SerializeField] private TextMeshProUGUI _sectionNameText;
        [SerializeField] private GameObject _itemsContainer;
        
        public string SectionNameText
        {
            get => _sectionName;
            set
            {
                _sectionNameText.text = value;
                _sectionName = value;
            } 
        }

        public Transform Container
        {
            get => _itemsContainer.transform;
        }
    }
}