using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProyectM2.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Section[] _infinitiveSection;
        private List<Section> _sectionsListInGame = new List<Section>();

        public int _currentIndex = 0;
        private bool _isInBonusLevel = false;

        public void SetFirstSection(Section firstSection)
        {
            firstSection.Suscribe(Notify);
            _sectionsListInGame.Add(firstSection);
        }

        private Section NewSection(int sectionIndex)
        {
            _currentIndex = Random.Range(0, _infinitiveSection.Length);
          
            Section newBlock = Instantiate(_infinitiveSection[_currentIndex], _sectionsListInGame[^1].transform.Find("CreateSectionPivot").position, Quaternion.identity);


            if (!_sectionsListInGame.Contains(_infinitiveSection[_currentIndex]))
            {
                _sectionsListInGame.Add(newBlock);
            }

            return newBlock;
        }

        public void Notify(Section section)
        {
            section.Unsuscribe(Notify);

            NewSection(_currentIndex).Suscribe(Notify);
            _sectionsListInGame.Remove(section);

        }
    }
}
