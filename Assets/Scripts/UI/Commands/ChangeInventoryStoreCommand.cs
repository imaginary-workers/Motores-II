using ProyectM2.Car;
using UnityEngine;

namespace ProyectM2.UI.Commands
{
    public class ChangeInventoryStoreCommand: ChangeMenuCommand
    {
        private readonly PlayerPersonalization _playerPersonalization;
        public ChangeInventoryStoreCommand(GameObject[] toShow, GameObject[] toHide, PlayerPersonalization playerPersonalization) : base(toShow, toHide)
        {
            _playerPersonalization = playerPersonalization;
        }

        public override void Undo()
        {
            base.Undo();
            _playerPersonalization.UpdateSkinToActive();
        }
    }
}