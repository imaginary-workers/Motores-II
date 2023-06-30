using ProyectM2.Persistence;
using UnityEngine;

namespace ProyectM2.Store
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

            instanciaClase.totalCurrencyOfPlayer -= (int)itemBought.Price;
            instanciaClase.totalCurrencyGainOfPlayer -= (int)itemBought.Price;
            DataPersistance.Instance.WriteJson(instanciaClase);
            EventManager.TriggerEvent("CurrencyModified");
        }

    }
}
