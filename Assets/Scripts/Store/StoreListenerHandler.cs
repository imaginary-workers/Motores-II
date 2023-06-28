using ProyectM2.Inventory;
using ProyectM2.Persistence;
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
            var itemBought = ItemProvider.Instance.FindSpecificItem((string)obj[0]);

            var instanciaClase = DataPersistance.Instance.LoadGame();

            instanciaClase.totalCurrencyGainOfPlayer -= (int)itemBought.Price;
            DataPersistance.Instance.WriteJson(instanciaClase);
            EventManager.TriggerEvent("CurrencyModified");
        }

    }
}
