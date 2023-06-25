namespace ProyectM2
{
    public interface INotification
    {
        public string Title { get; }
        public string Description { get; }
        public string IconId { get; }
        public float RepeatNotificationInHours { get; }
    }
}