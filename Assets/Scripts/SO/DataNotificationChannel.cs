using Unity.Notifications.Android;
using UnityEngine;

namespace ProyectM2.SO
{
    [CreateAssetMenu(fileName = "NotificationChannelSO", menuName = "SO/NotificationChannel", order = 0)]
    public class DataNotificationChannel : ScriptableObject, INotificationChannel
    {
        [SerializeField] private string channelId;
        [SerializeField] private string channelInternalName;
        [SerializeField] private string channelDescription;
        [SerializeField] private Importance importanceChannel;

        public string ChannelId { get => channelId; }
        public string ChannelInternalName { get => channelInternalName;  }
        public string ChannelDescription { get => channelDescription;  }
        public Importance ImportanceChannel { get => importanceChannel;  }
    }
}
