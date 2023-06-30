using ProyectM2.Stamina;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.Advertisements;

namespace ProyectM2.Ads
{
    public class AdsManager : Singleton<AdsManager>, IUnityAdsLoadListener, IUnityAdsShowListener,
        IUnityAdsInitializationListener
    {
        private AdsConfig _adsConfig;

        protected override void Awake()
        {
            base.Awake();
            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        }

        private void OnDestroy()
        {
            RemoteConfigService.Instance.FetchCompleted -= ApplyRemoteSettings;
        }

        #region Initialization
        private void ApplyRemoteSettings(ConfigResponse obj)
        {
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
#if UNITY_IOS
                var keyConfig = "ios-config";
#elif UNITY_ANDROID
                var keyConfig = "android-config";
#elif UNITY_EDITOR
                var keyConfig = "android-config";
#endif
                _adsConfig = new AdsConfig();
                JsonUtility.FromJsonOverwrite(RemoteConfigService.Instance.appConfig.GetJson(keyConfig), _adsConfig);
                Advertisement.Initialize(_adsConfig.game_id, _adsConfig.test, this);
            }
        }

        public void OnInitializationComplete()
        {
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Error initializating Ad Services: {error.ToString()} - {message}");
        }
        #endregion

        #region Load Ads

        [ContextMenu("Load Ad")]
        public void LoadRewardedAd()
        {
            Advertisement.Load(_adsConfig.rewarded, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.LogWarning($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        #endregion

        #region Show Ads

        [ContextMenu("Show Ad")]
        public void ShowRewardedAd()
        {
            Advertisement.Show(_adsConfig.rewarded, this);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adsConfig.rewarded) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                StaminaSystem.Instance.RechargeStamina(1);
            }

            if (adUnitId.Equals(_adsConfig.rewarded) && showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
            {
                // Cancel reward.
            }
        }


        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.LogWarning($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }
        #endregion
    }
}
