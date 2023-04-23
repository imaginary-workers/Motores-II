using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2.UI
{
    public class LoadCanvasUI : MonoBehaviour
    {
        [SerializeField] private GameObject _loadScreen;
        [SerializeField] private TextMeshProUGUI _loadText;
        [SerializeField] private Slider _loadBar;

        public void DisplayLoadCanvas(bool display)
        {
            _loadScreen.SetActive(display);
        }

        public void SetLoadTextTo(string loadText)
        {
            _loadText.text = loadText;
        }

        public void SetLoadBarTo(float loadValue)
        {
            _loadBar.value = loadValue;
        }
    }
}