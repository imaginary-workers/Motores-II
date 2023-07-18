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
        private int currentStamina;
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

            if (currentStamina < maxStamina)
            {
                SendNotification();
            }
        }

        IEnumerator UpdateStamina()
        {
            UpdateTimer();
            recharging = true;
            while (currentStamina < maxStamina)
            {
                DateTime currentTime = DateTime.Now;
                DateTime nextTime = nextStaminaTime;


                bool addingStamina = false;
                while (currentTime > nextTime)
                {
                    if (currentStamina >= maxStamina) break;


                    currentStamina += 1;
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
            if (currentStamina >= maxStamina)
            {
                if (recharging)
                {
                    recharging = false;
                    StopAllCoroutines();
                }
            }

            currentStamina += staminaToAdd;

            SendNotification();

            UpdateUI();
            UpdateTimer();
            Save();
        }

        public void UseStamina(int staminaToUse)
        {
            if (!HasEnoughStamina(staminaToUse)) return;

            currentStamina -= staminaToUse;

            UpdateUI();
            
            if (currentStamina < maxStamina)
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

        public bool HasEnoughStamina(int stamina) => currentStamina >= stamina;

        void UpdateUI()
        {
            myStaminaUI.UpdateStamina(currentStamina, maxStamina);
        }

        void UpdateTimer()
        {
            myStaminaUI.UpdateTimer(nextStaminaTime);
        }

        void Save()
        {
            myStaminaData.SaveStaminaData(currentStamina, nextStaminaTime.ToString(), lastStaminaTime.ToString());
        }

        void Load()
        {
            myStaminaData.LoadStaminaData();
            currentStamina = myStaminaData.CurrentStamina == -1 ? maxStamina : myStaminaData.CurrentStamina;
            nextStaminaTime = myStaminaData.NextStaminaTime;
            lastStaminaTime = myStaminaData.LastStaminaTime;
        }

        private void SendNotification()
        {

            NotificationSystem.Instance.CancelNotification(fullStaminaNotificationId);

            fullStaminaNotificationId = NotificationSystem.Instance.SendNotification(
                dataNotificationSO,
                AddDuration(nextStaminaTime, timeToCharge * (maxStamina - currentStamina - 1)),
                dataNotificationChannelSO
                );
        }

        DateTime AddDuration(DateTime date, float duration)
        {
            return date.AddSeconds(duration);
        }
    }
}