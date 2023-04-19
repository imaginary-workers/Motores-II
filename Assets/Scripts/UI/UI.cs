using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProyectM2
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private int _coin;
        public TextMesh _coinText;
        void Update()
        {
            _coinText.text = "Score: " + _coin.ToString();
        }
        
    }
}