using System;
using System.Collections.Generic;
using UnityEngine;
using ProyectM2.Persistence;

namespace ProyectM2.Managers.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Sections[] _sections;
        [SerializeField] private Sections _infinitiveSection;
        private List<Sections> _sectionsListInGame;
        public int _currentIndex = 0;
        public bool _isInInfinitiveSection = false;
        private bool _isInBonusLevel = false;


        private void Awake()
        {
            NewSection(_currentIndex);
            if (SessionGameData.GetData("IsInBonusLevel")!=null)
            {
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");
            }
               
        }
        private void Start()
        {
            _sectionsListInGame = new List<Sections>();

            if (SessionGameData.GetData("LastSectionIndex") != null &&  !_isInBonusLevel)
            {
                for (int i = _currentIndex; i < (int)SessionGameData.GetData("LastSectionIndex"); i++)
                {
                    NewSection(i);
                }
            }
        }

        void NewSection(int sectionIndex)
        {
            if (_isInInfinitiveSection)
            {
                Instantiate(_infinitiveSection, _sectionsListInGame[_sectionsListInGame.Count - 1].transform.Find("CreateSectionPivot").position, Quaternion.identity);
            }
            else if (sectionIndex <= _sections.Length - 1)
            {
                if (sectionIndex == 0)
                    Instantiate(_sections[sectionIndex], new Vector3(0, 0, 0), Quaternion.identity);
                else
                    Instantiate(_sections[sectionIndex], _sectionsListInGame[Math.Max(0, _sectionsListInGame.Count - 1)].transform.Find("CreateSectionPivot").position, Quaternion.identity);

                _currentIndex++;
            }

            if (!_sectionsListInGame.Contains(_sections[sectionIndex]))
            {
                _sectionsListInGame.Add(_sections[sectionIndex]);
                _sections[sectionIndex].Suscribe(Notify);
            }
        }

        public void Notify(string action)
        {
            if (action == "CreateSection")
            {
                NewSection(_currentIndex);
                if (!_isInInfinitiveSection)
                {
                    _sections[_currentIndex-1].Unsuscribe(Notify);
                    _sectionsListInGame.Remove(_sections[_currentIndex-1]);
                }
                else
                {
                    _infinitiveSection.Unsuscribe(Notify);
                }
            }
        }
    }
}
