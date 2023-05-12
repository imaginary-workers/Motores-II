using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProyectM2.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Section[] _infinitiveSection;
        private List<Section> _sectionsListInGame;
        public int _currentIndex = 0;
        private bool _isInBonusLevel = false;

        public void SetFirstSection(Section firstSection)
        {
            firstSection.Suscribe(Notify);
            _sectionsListInGame.Add(firstSection);
        }

        private void NewSection(int sectionIndex)
        {
            _currentIndex = Random.Range(0, _infinitiveSection.Length);
            Instantiate(_infinitiveSection[_currentIndex], _sectionsListInGame[^1].transform.Find("CreateSectionPivot").position, Quaternion.identity);

            if (!_sectionsListInGame.Contains(_infinitiveSection[_currentIndex]))
            {
                _sectionsListInGame.Add(_infinitiveSection[_currentIndex]);
            }
        }

        public void Notify(Section section)
        {
            section.Unsuscribe(Notify);
            _sectionsListInGame.Remove(section);
            NewSection(_currentIndex);
            _infinitiveSection[_currentIndex].Suscribe(Notify);
        }
    }
}
