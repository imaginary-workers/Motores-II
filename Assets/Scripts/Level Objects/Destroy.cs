using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class Destroy : MonoBehaviour
    {
        string _sectionsPrefabsName;

        private void Start()
        {
            _sectionsPrefabsName = transform.name;
            StartCoroutine(DestroySection());
        }

        IEnumerator DestroySection()
        {
            yield return new WaitForSeconds(3);
            if (_sectionsPrefabsName.Split(" ")[0] == "Section" || _sectionsPrefabsName == "Section(Clone)")
            {
                Destroy(gameObject);
            }
        }
    }
}
