using System;
using System.Threading.Tasks;
using ProyectM2.Ads;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace ProyectM2.Config
{
    public class RemoteConfigManager : MonoBehaviour
    {
        public struct userAttributes {}
        public struct appAttributes {}

        private async Task Awake()
        {
            if (Utilities.CheckForInternetConnection())
            {
                await InitializeRemoteConfigAsync();
            }
            else
            {
                Debug.LogError("No connection to internet");
            }
        }

        private async Task Start()
        {
            await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
        }

        private async Task InitializeRemoteConfigAsync()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
    }
}