using System;

namespace ProyectM2.Managers
{
    [Serializable]
    public struct Scene
    {
        public enum Type {Gameplay, Menu}
        public string name;
        public Type type;

        public Scene(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }
    }
}