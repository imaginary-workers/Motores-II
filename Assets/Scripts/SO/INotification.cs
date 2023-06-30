namespace ProyectM2.SO
{
    public interface INotification
    {
        public string Title { get; }
        public string Description { get; }
        public string IconId { get; }
        public float RepeatNotificationInHours { get; }
    }
}