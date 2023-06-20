using UnityEngine;
using System;
using System.Collections;

namespace ProyectM2
{
    public class StaminaSystem : MonoBehaviour
    {

        [SerializeField] int maxStamina = 10;
        int currentStamina;
        DateTime nextStaminaTime;
        DateTime lastStaminaTime;
        [SerializeField] float timeToCharge = 10f;

        bool recharging;

        private void Start()
        {
            if (PlayerPrefs.HasKey("currentStamina"))
            {
                Load();
            }
            else
            {
                currentStamina = maxStamina;
                Save();
            }

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

        void UpdateTimer()
        {
            if (currentStamina >= maxStamina)
            {
                EventManager.TriggerEvent("UpdateStaminaTimerUI", "FullStamina");
                return;
            }

            var timer = nextStaminaTime - DateTime.Now;
            var hoursAndMinutes = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
            EventManager.TriggerEvent("UpdateStaminaTimerUI", hoursAndMinutes);

        }

        public void RechargeStamina(int staminaToAdd)
        {
            currentStamina += staminaToAdd;

            UpdateUI();
            Save();
            if (currentStamina >= maxStamina)
            {
                if (recharging)
                {
                    recharging = false;
                    StopAllCoroutines();
                }
            }
        }

        public void UseStamina(int staminaToUse)
        {
            if (!HasEnoughStamina(staminaToUse))
            {
                Debug.Log("No tengo suficiente stamina");
                return;
            }

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

        DateTime AddDuration(DateTime date, float duration)
        {
            return date.AddSeconds(duration);
        }

        bool HasEnoughStamina(int stamina) => currentStamina >= stamina;

        void UpdateUI()
        {
            var staminaText = currentStamina.ToString() + "/" + maxStamina.ToString();
            EventManager.TriggerEvent("UpdateStaminaUI", staminaText);
        }

        void Save()
        {
            PlayerPrefs.SetInt("currentStamina", currentStamina);
            PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
            PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
        }

        void Load()
        {
            currentStamina = PlayerPrefs.GetInt("currentStamina");
            nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
            lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));
        }

        DateTime StringToDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.Now;
            }

            return DateTime.Parse(date);
        }
    }
}
