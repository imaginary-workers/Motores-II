using UnityEngine;
using System;
using System.Collections;
using ProyectM2.Persistence;

namespace ProyectM2
{
    public class StaminaSystem : MonoBehaviour
    {

        [SerializeField] private int maxStamina = 10;
        private int currentStamina;
        private DateTime nextStaminaTime;
        private DateTime lastStaminaTime;
        [SerializeField] private float timeToCharge = 10f;

        private bool recharging;

        private StaminaData myStaminaData;
        private StaminaUI myStaminaUI;

        private void Awake()
        {
            myStaminaData = GetComponent<StaminaData>();
            myStaminaUI = GetComponent<StaminaUI>();
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

        public bool CanUseStamina(int staminaToUse)
        {

            if (!HasEnoughStamina(staminaToUse))
                return false;


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
            return true;
        }

        bool HasEnoughStamina(int stamina) => currentStamina >= stamina;

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
