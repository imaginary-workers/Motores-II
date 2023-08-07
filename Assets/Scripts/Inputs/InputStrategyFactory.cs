using UnityEngine;

namespace ProyectM2.Inputs
{
    public class InputStrategyFactory: Singleton<InputStrategyFactory>
    {
        [SerializeField] private GameObject _screenButtonsPrefab;

        protected override void Awake()
        {
            itDestroyOnLoad = true;
            base.Awake();
        }

        public InputStrategy CreateInputStrategy(InputType type)
        {
            GameObject strategyGO;
            switch (type)
            {
                case InputType.KeywordMouse:
                    strategyGO = new GameObject(type.ToString());
                    strategyGO.AddComponent<KeyboardMouseStrategy>();
                    break;
                case InputType.ScreenButton:
                    strategyGO = Instantiate(_screenButtonsPrefab, Vector3.zero, Quaternion.identity);
                    break;
                default:
                    strategyGO = new GameObject(type.ToString());
                    strategyGO.AddComponent<TactilStrategy>();
                    break;
            }

            return strategyGO.GetComponent<InputStrategy>();
        }
    }
}