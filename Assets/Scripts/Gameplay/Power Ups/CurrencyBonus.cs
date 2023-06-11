using ProyectM2.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace ProyectM2
{
    public class CurrencyBonus : MonoBehaviour
    {
        bool bonus { get; }
        DataInt data;

        void Update()
        {
            if (bonus)
            {
                data.value = 2;
            }
            else data.value = 1;
        }
    }
}
