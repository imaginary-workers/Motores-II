using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Borrar : MonoBehaviour
    {
        public Transform[] despues;
        public Transform[] antes;

        [ContextMenu("Borrar")]
        public void BorrarM()
        {
            for (int i = 0; i < despues.Length; i++)
            {
                var ante = antes[i];
                var despue = despues[i];
                despue.position = ante.position;
                ante.GetChild(0).SetParent(despue.GetChild(0).GetChild(0));
                despue.GetChild(0).GetChild(0).GetChild(0).localPosition = Vector3.zero;
            }
        }
    }
}
