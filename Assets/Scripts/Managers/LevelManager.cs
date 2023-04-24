using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class LevelManager : MonoBehaviour, IObserver
    {
        [SerializeField] Sections[] _sections;
        List<Sections> _sectionsListInGame;
        Sections[] _allSectionsInGame;
        [SerializeField] int _currentIndex;


        private void Start()
        {
            _currentIndex = 0;
            _sectionsListInGame = new List<Sections>();
            NewSection(_currentIndex);
        }

        private void Update()
        {
            _allSectionsInGame = FindObjectsOfType<Sections>();
            foreach (Sections section in _allSectionsInGame)
            {
                if (!_sectionsListInGame.Contains(section))
                {
                    _sectionsListInGame.Add(section);
                    section.Suscribe(this);
                }
            }
        }

        void NewSection(int sectionIndex)
        {
            if (_currentIndex < _sections.Length)
            {
                if (_currentIndex == 0)
                    Instantiate(_sections[sectionIndex], new Vector3(0,0,0), Quaternion.identity);
                else
                    Instantiate(_sections[sectionIndex], _sectionsListInGame[sectionIndex-1].transform.Find("CreateSectionPivot").position, Quaternion.identity);
                _currentIndex++;
            }
        }


        public void Notify(string action)
        {
            if (action == "Create Section")
            {
                
                _sections[_currentIndex - 1].Unsuscribe(this);
                NewSection(_currentIndex);
                _sectionsListInGame.Remove(_sections[_currentIndex - 1]);
            }

        }
    }
}
