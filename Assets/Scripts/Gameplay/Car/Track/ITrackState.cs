namespace ProyectM2.Gameplay.Car.Track
{
    public interface ITrackState
    {
        public void MoveRight();
        public void MoveLeft();
        public bool CanMoveRight();
        public bool CanMoveLeft();
    }
}