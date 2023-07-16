using Unity.Notifications.Android;

namespace ProyectM2.SO
{
    public interface INotificationChannel
    {
        public string ChannelId { get; }
        public string ChannelInternalName { get; }
        public string ChannelDescription { get; }
        public Importance ImportanceChannel { get; }
    }
}