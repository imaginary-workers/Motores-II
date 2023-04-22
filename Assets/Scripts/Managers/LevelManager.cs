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
        [SerializeField] int _currentIndex;

        private void Start()
        {
            _currentIndex = 0;
            NewSection(_currentIndex);

        }

        private void Update()
        {
            if (!_creatingSection)
            {
                _creatingSection = true;
                //StartCoroutine(GenerateSection());
            }

            if (Input.GetKey(KeyCode.A))
            {
                NewSection(_currentIndex);
            }
        }

        //IEnumerator GenerateSection()
        //{
        //    _sectionIndex = Random.Range(0, _sections.Length);
        //    Instantiate(_sections[_sectionIndex], new Vector3(0, 1.5f, _zPos), Quaternion.identity);
        //    _zPos += 50f;
        //    yield return new WaitForSeconds(2);
        //    _creatingSection = false;
        //}

        void NewSection(int sectionIndex)
        {
            if (_currentIndex == 0)
                Instantiate(_sections[sectionIndex], new Vector3(0,0,0), Quaternion.identity);
            else
                Instantiate(_sections[sectionIndex], _sections[sectionIndex - 1].transform.Find("Pivot").transform.position, Quaternion.identity);
            _currentIndex += 1;
        }

        void DeleteSection(int sectionIndex)
        {
        }

    }
}
