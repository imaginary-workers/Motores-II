using ProyectM2.Gameplay.Car.Player;
using UnityEngine;

namespace ProyectM2.UI.Commands
{
    public class ChangeInventoryStoreCommand: ChangeMenuCommand
    {
        private readonly PlayerPersonalization _playerPersonalization;
        private readonly GameObject _popUpWindow;

        public ChangeInventoryStoreCommand(GameObject[] toShow, GameObject[] toHide, PlayerPersonalization playerPersonalization, GameObject popUpWindow = null) : base(toShow, toHide)
        {
            _playerPersonalization = playerPersonalization;
            _popUpWindow = popUpWindow;
        }

        public override void Undo()
        {
            base.Undo();
            _playerPersonalization.UpdateSkinToActive();
            _popUpWindow?.SetActive(false);
        }
    }
}