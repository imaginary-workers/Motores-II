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

        [Header("Dependencies")] [SerializeField]
        private StaminaData myStaminaData;

        [SerializeField] private StaminaUI myStaminaUI;
        public int CurrentStamina { get; private set; }
        private DateTime nextStaminaTime;
        private DateTime lastStaminaTime;

        private bool recharging;

        private int fullStaminaNotificationId = -1;


        protected override void Awake()
        {
            itDestroyOnLoad = true;
            base.Awake();
        }

        private void Start()
        {
            Load();
            UpdateUI();
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

                UpdateUI();
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
            EventManager.TriggerEvent("UpdateStamina");
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
            EventManager.TriggerEvent("UpdateStamina");
        }

        public bool HasEnoughStamina(int stamina) => CurrentStamina >= stamina;

        void UpdateUI()
        {
            myStaminaUI.UpdateStamina(CurrentStamina, maxStamina);
        }

        void UpdateTimer()
        {
            myStaminaUI.UpdateTimer(nextStaminaTime);
        }

        void Save()
        {
            myStaminaData.SaveStaminaData(CurrentStamina, nextStaminaTime.ToString(), lastStaminaTime.ToString());
        }

        void Load()
        {
            myStaminaData.LoadStaminaData();
            CurrentStamina = myStaminaData.CurrentStamina == -1 ? maxStamina : myStaminaData.CurrentStamina;
            nextStaminaTime = myStaminaData.NextStaminaTime;
            lastStaminaTime = myStaminaData.LastStaminaTime;
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