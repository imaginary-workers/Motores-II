using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _sections;
        [SerializeField] float _zPos = 50;
        [SerializeField] bool _creatingSection = false;
        [SerializeField] int _sectionIndex;
        [SerializeField] int _currentIndex;

        private void Start()
        {
            _sectionIndex = 0;
            _currentIndex = _sectionIndex;

        }

        private void Update()
        {
            if (!_creatingSection)
            {
                _creatingSection = true;
                //StartCoroutine(GenerateSection());
            }
        }

        IEnumerator GenerateSection()
        {
            _sectionIndex = Random.Range(0, _sections.Length);
            Instantiate(_sections[_sectionIndex], new Vector3(0, 1.5f, _zPos), Quaternion.identity);
            _zPos += 50f;
            yield return new WaitForSeconds(2);
            _creatingSection = false;
        }

        void NewSection(int sectionIndex)
        {
            Instantiate(_sections[sectionIndex], _sections[sectionIndex - 1].transform.position, Quaternion.identity);
        }

        void DeleteSection(int sectionIndex)
        {

        }

    }
}
