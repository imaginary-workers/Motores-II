using UnityEngine;

namespace ProyectM2.Personalization
{
    public class SetMaterialSkins : MonoBehaviour
    {
        public Renderer rendererChasis;
        public Renderer[] rendererWheels;
        public Renderer rendererGlass;

        public void SetMaterialChasis(Material material)
        {
            rendererChasis.material = material;
        }
        public void SetColorWheels(Color color)
        {
            foreach (var renderer in rendererWheels)
            {
                renderer.material.color = color;
            }
        }
        public void SetColorGlass(Color color)
        {
            rendererGlass.material.color = color;
        }
    }
}
