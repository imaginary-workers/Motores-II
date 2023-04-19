using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class BorrarMonedas : MonoBehaviour
    {
        [SerializeField] static int monedas;

        public static void AddCurrency()
        {
            monedas++;
            Debug.Log("Tenes " + monedas + " Monedas ");
        }
    }
}
