using UnityEngine;
using System;
using System.Collections;

namespace ProyectM2
{
    public class StaminaSystem : Singleton<StaminaSystem>
    {

        [SerializeField] private int maxStamina = 10;
        [SerializeField] private float timeToCharge = 10f;
        [SerializeField] private StaminaData myStaminaData;
        [SerializeField] private StaminaUI myStaminaUI;
        private int currentStamina;
        private DateTime nextStaminaTime;
        private DateTime lastStaminaTime;

        private bool recharging;


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
                    UpdateTimer();
                    recharging = false;
                    StopAllCoroutines();
                }
                return;
            }
            currentStamina += staminaToAdd;

            UpdateUI();
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
                    Save();
                    StartCoroutine(UpdateStamina());
                }
            }
        }

        public bool HasEnoughStamina(int stamina) => currentStamina >= stamina;

        void UpdateUI()
        {
            if (myStaminaUI != null)
                myStaminaUI.UpdateStamina(currentStamina, maxStamina);
        }

        void UpdateTimer()
        {
            if (myStaminaUI != null)
                myStaminaUI.UpdateTimer(nextStaminaTime);
        }

        void Save()
        {
            if (myStaminaData != null)
                myStaminaData.SaveStaminaData(currentStamina, nextStaminaTime.ToString(), lastStaminaTime.ToString());
        }

        void Load()
        {
            if (myStaminaData != null)
            {
                myStaminaData.LoadStaminaData();
                currentStamina = myStaminaData.CurrentStamina;
                nextStaminaTime = myStaminaData.NextStaminaTime;
                lastStaminaTime = myStaminaData.LastStaminaTime;
            }
        }

        DateTime AddDuration(DateTime date, float duration)
        {
            return date.AddSeconds(duration);
        }
    }
}
