using UnityEngine;
using System;
using System.Collections;
using ProyectM2.SO;
using ProyectM2.Notifications;

namespace ProyectM2.Stamina
{
    public class StaminaSystem : Singleton<StaminaSystem>
    {

        [SerializeField] private int maxStamina = 10;
        [SerializeField] private float timeToCharge = 10f;
        [SerializeField] private DataNotification dataNotificationSO; 
        [SerializeField] private DataNotificationChannel dataNotificationChannelSO;

        public int CurrentStamina { get; private set; }
        private DateTime nextStaminaTime;
        private DateTime lastStaminaTime;

        private bool recharging;

        private int fullStaminaNotificationId = -1;

        private void OnEnable()
        {
            EventManager.StartListening("SceneLoadComplete", UpdateUIByEvent);
            EventManager.StartListening("LoseCanvasActive", UpdateUIByEvent);
        }

        private void OnDisable()
        {
            EventManager.StopListening("SceneLoadComplete", UpdateUIByEvent);
            EventManager.StopListening("LoseCanvasActive", UpdateUIByEvent);
        }

        private void UpdateUIByEvent(object[] obj)
        {
            Load();
            UpdateUI();
            UpdateTimer();
        }

        private void Start()
        {
            Load();
            UpdateUI();
            UpdateTimer();
            StartCoroutine(UpdateStamina());

            if (CurrentStamina < maxStamina)
            {
                SendNotification();
            }
        }

        IEnumerator UpdateStamina()
        {
            UpdateTimer();
            recharging = true;
            while (CurrentStamina < maxStamina)
            {
                DateTime currentTime = DateTime.Now;
                DateTime nextTime = nextStaminaTime;


                bool addingStamina = false;
                while (currentTime > nextTime)
                {
                    if (CurrentStamina >= maxStamina) break;


                    CurrentStamina += 1;
                    UpdateUI();
                    addingStamina = true;
                    DateTime timeToAdd = nextTime;

                    if (lastStaminaTime > nextTime)
                        timeToAdd = lastStaminaTime;

                    nextTime = AddDuration(timeToAdd, timeToCharge);
                }

                if (addingStamina)
                {
                    nextStaminaTime = nextTime;
                    lastStaminaTime = DateTime.Now;
                }

                UpdateTimer();
                Save();

                yield return new WaitForEndOfFrame();
            }

            recharging = false;
        }

        public void RechargeStamina(int staminaToAdd)
        {           
            if (CurrentStamina >= maxStamina)
            {
                if (recharging)
                {
                    recharging = false;
                    StopAllCoroutines();
                }
            }

            CurrentStamina += staminaToAdd;

            SendNotification();

            UpdateUI();
            UpdateTimer();
            Save();
            EventManager.TriggerEvent("RechargeStamina");
        }

        public void UseStamina(int staminaToUse)
        {
            if (!HasEnoughStamina(staminaToUse)) return;

            CurrentStamina -= staminaToUse;

            UpdateUI();
            
            if (CurrentStamina < maxStamina)
            {

                if (!recharging)
                {
                    nextStaminaTime = AddDuration(DateTime.Now, timeToCharge);
                    StartCoroutine(UpdateStamina());
                }
                SendNotification();
            }
            Save();
        }

        public bool HasEnoughStamina(int stamina) => CurrentStamina >= stamina;

        void UpdateUI()
        {
            EventManager.TriggerEvent("UpdateStamina", CurrentStamina, maxStamina);
        }

        void UpdateTimer()
        {
            EventManager.TriggerEvent("ModifyStaminaTimer", nextStaminaTime);
        }

        void Save()
        {
            StaminaData.Instance.SaveStaminaData(CurrentStamina, nextStaminaTime.ToString(), lastStaminaTime.ToString());
        }

        void Load()
        {
            StaminaData.Instance.LoadStaminaData();
            CurrentStamina = StaminaData.Instance.CurrentStamina == -1 ? maxStamina : StaminaData.Instance.CurrentStamina;
            nextStaminaTime = StaminaData.Instance.NextStaminaTime;
            lastStaminaTime = StaminaData.Instance.LastStaminaTime;
        }

        private void SendNotification()
        {

            NotificationSystem.Instance.CancelNotification(fullStaminaNotificationId);

            fullStaminaNotificationId = NotificationSystem.Instance.SendNotification(
                dataNotificationSO,
                AddDuration(nextStaminaTime, timeToCharge * (maxStamina - CurrentStamina - 1)),
                dataNotificationChannelSO
                );
        }

        DateTime AddDuration(DateTime date, float duration)
        {
            return date.AddSeconds(duration);
        }
    }
}