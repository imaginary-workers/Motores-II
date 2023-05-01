using UnityEngine;

namespace ProyectM2.Sound
{
    public class UISoundController : MonoBehaviour
    {
        public void PlayClip(AudioClip clip)
        {
            UISoundManager.Instance.PlaySound(clip);
        }
    }
}
