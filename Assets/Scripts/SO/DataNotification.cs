using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "NotificationSO", menuName = "SO/Notification", order = 0)]
    public class DataNotification : ScriptableObject, INotification
    {
        [SerializeField] private string title;
        [SerializeField, TextArea(1, 2)] private string description;
        [SerializeField] private string iconId;
        [SerializeField] private float repeatNotificationInHours = 0f;

        public string Title { get => title; }
        public string Description { get => description; }
        public string IconId { get => iconId; }
        public float RepeatNotificationInHours { get => repeatNotificationInHours; }
    }
}