using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2
{
    public class StoreListenerHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.StartListening("BuyItem", BuyItemHandler);
            EventManager.StartListening("UseItem", UseItemHandler);
        }


        private void OnDisable()
        {
            EventManager.StopListening("BuyItem", BuyItemHandler);
            EventManager.StopListening("UseItem", UseItemHandler);
        }
        private void UseItemHandler(object[] obj)
        {
            ItemHandler((IStoreItem)obj[0], "Used");
        }

        private void BuyItemHandler(object[] obj)
        {
            ItemHandler((IStoreItem)obj[0], "Buy");
        }

        private void ItemHandler(IStoreItem item, string transactionType)
        {
            var itemBought = item;
            itemBought = ItemProvider.FindSpecificItem(itemBought.Name);
            DataPersistance.Instance.UpdateStoreData(itemBought, transactionType);
        }
    }
}
