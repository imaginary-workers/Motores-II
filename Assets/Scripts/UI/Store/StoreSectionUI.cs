using TMPro;
using UnityEngine;

namespace ProyectM2.UI.Store
{
    public class StoreSectionUI: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sectionNameText;
        [SerializeField] private GameObject _itemsContainer;
        
        public string SectionNameText
        {
            get => _sectionNameText.text;
            set => _sectionNameText.text = value;
        }

        public Transform Container
        {
            get => _itemsContainer.transform;
        }

        public void AddItem(GameObject item)
        {
            item.transform.SetParent(_itemsContainer.transform);
        }
    }
}