using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProyectM2.Gameplay;
using ProyectM2.Persistence;

namespace ProyectM2.Managers.Levels
{
    public class LevelManager : MonoBehaviour, IObserver
    {
        [SerializeField] Sections[] _sections;
        [SerializeField] Sections _infinitiveSection;
        List<Sections> _sectionsListInGame;
        Sections[] _allSectionsInGame;
        [SerializeField] int _currentIndex = 0;
        [SerializeField] bool _isInInfinitiveSection = false;
        [SerializeField] bool _isInBonusLevel = false;


        private void Awake()
        {
            NewSection(_currentIndex);
            if (SessionGameData.GetData("IsInBonusLevel")!=null)
            {
                _isInBonusLevel = (bool)SessionGameData.GetData("IsInBonusLevel");
            }
               
        }

        private void OnEnable()
        {
            EventManager.StartListening("EnemyCutSceneStarted", NewInfinitiveSection);
            EventManager.StartListening("EnemyDiedCutSceneStarted", DisableInfinitiveSection);
            EventManager.StartListening("TeleportToBonusLevel", GoToBonusLevel);
            EventManager.StartListening("TeleportReturnToLevel", GoToBonusLevel);
        }


        private void OnDisable()
        {
            EventManager.StopListening("EnemyCutSceneStarted", NewInfinitiveSection);
            EventManager.StopListening("EnemyDiedCutSceneStarted", DisableInfinitiveSection);
            EventManager.StopListening("TeleportToBonusLevel", GoToBonusLevel);
            EventManager.StopListening("TeleportReturnToLevel", GoToBonusLevel);

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

        private void GoToBonusLevel(object[] obj)
        {
            SessionGameData.SaveData("LastSectionIndex", _currentIndex - 1);
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
        }

        void NewInfinitiveSection(object[] obj)
        {
            _isInInfinitiveSection = true;
        }

        void DisableInfinitiveSection(object[] obj)
        {
            _isInInfinitiveSection = false;
        }

        public void Notify(string action)
        {
            if (action == "CreateSection")
            {
                NewSection(_currentIndex);
                if (!_isInInfinitiveSection)
                {
                    _sections[_currentIndex-1].Unsuscribe(this);
                    _sectionsListInGame.Remove(_sections[_currentIndex-1]);
                }
                else
                {
                    _infinitiveSection.Unsuscribe(this);
                }
            }
        }
    }
}
