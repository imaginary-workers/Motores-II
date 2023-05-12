namespace ProyectM2.UI.Commands
{
    public interface ICommand {
        void Execute();
        void Undo();
    }
}