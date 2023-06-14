using ProyectM2.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProyectM2
{
    public class StoreListenerHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.StartListening("BuyItem", BuyItemHandler);
        }

        private void OnDisable()
        {
            EventManager.StopListening("BuyItem", BuyItemHandler);
        }

        private void BuyItemHandler(object[] obj)
        {
            var itemBought = (IStoreItem)obj[0];
            itemBought = ItemProvider.FindSpecificItem(itemBought.Name);
            DataPersistance.Instance.UpdateStoreData(itemBought, "Buy");
        }
    }
}
