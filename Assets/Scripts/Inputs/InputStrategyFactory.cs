using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputStrategyFactory: Singleton<InputStrategyFactory>
    {
        [SerializeField] private GameObject _screenButtons;

        protected override void Awake()
        {
            itDestroyOnLoad = true;
            base.Awake();
            _screenButtons = FindObjectOfType<ScreemButtonsStrategy>().gameObject;
            _screenButtons.SetActive(false);
        }

        public InputStrategy CreateInputStrategy(InputType type)
        {
            GameObject strategyGO;
            switch (type)
            {
                case InputType.KeywordMouse:
                    strategyGO = new GameObject(type.ToString());
                    strategyGO.AddComponent<KeyboardMouseStrategy>();
                    strategyGO.transform.SetParent(transform);
                    break;
                case InputType.ScreenButton:
                    strategyGO = _screenButtons;
                    strategyGO.SetActive(true);
                    break;
                default:
                    strategyGO = new GameObject(type.ToString());
                    strategyGO.AddComponent<TactilStrategy>();
                    strategyGO.transform.SetParent(transform);
                    break;
            }

            return strategyGO.GetComponent<InputStrategy>();
        }
    }
}