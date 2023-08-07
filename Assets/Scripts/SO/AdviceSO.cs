using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProyectM2
{
    [CreateAssetMenu(fileName = "NewAdvice", menuName = "SO/Advice", order = 0)]
    public class AdviceSO : ScriptableObject
    {
        public List<string> advice = new List<string>();       
    }
}
