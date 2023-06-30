using Unity.Notifications.Android;
using System;

namespace ProyectM2.Notifications
{
    public class NotificationSystem : Singleton<NotificationSystem>
    {

        private void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }

        public int SendNotification(INotification notificationData, DateTime schedule, INotificationChannel channel)
        {
            var channelId = GetChannelId(channel);
            var notification = new AndroidNotification
            {
                Title = notificationData.Title,
                Text = notificationData.Description,
                SmallIcon = notificationData.IconId,
                FireTime = schedule
            };

            if (notificationData.RepeatNotificationInHours != 0)
                notification.RepeatInterval = TimeSpan.FromHours(notificationData.RepeatNotificationInHours); 
            return AndroidNotificationCenter.SendNotification(notification, channelId);
        }

        private string GetChannelId(INotificationChannel myChannelId)
        {
            var channelId = myChannelId.ChannelId;
            var notificationChannels = AndroidNotificationCenter.GetNotificationChannels();
            foreach (var notificationCh in notificationChannels)
            {
                if (notificationCh.Id == channelId)
                    return channelId;
            }

            var newChannel = new AndroidNotificationChannel
            {
                Id = myChannelId.ChannelId,
                Name = myChannelId.ChannelInternalName,
                Description = myChannelId.ChannelDescription,
                Importance = myChannelId.ImportanceChannel
            };

            AndroidNotificationCenter.RegisterNotificationChannel(newChannel);
            return newChannel.Id;
        }

        public void CancelNotification(int notificationId)
        {
            AndroidNotificationCenter.CancelNotification(notificationId);
        }
    }
}
