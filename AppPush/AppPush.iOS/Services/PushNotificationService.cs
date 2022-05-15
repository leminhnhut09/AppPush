using AppPush.Logics.Services.Interfaces;
using Firebase.CloudMessaging;
using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace AppPush.iOS.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public void CreateNotification(string title, string message)
        {
            
        }

        public Task<PermissionResult> GetGrantedState()
        {
            var settings = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
            var isGranted = settings != UIUserNotificationType.None;
            var rs = new PermissionResult
            {
                IsGranted = isGranted
            };
            return Task.FromResult(rs);
        }

        public Task<string> GetTokenAsync()
        {
            var token = Messaging.SharedInstance.FcmToken;
            Debug.WriteLine($"fcm token: {token}");
            return Task.FromResult(token);
        }

        public void RegisterRemoteNotification()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                //// For iOS 10 display notification (sent via APNS)
                //UNUserNotificationCenter.Current.Delegate = new MainNotificationDelegate();

                //var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                //UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                //{
                //});
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }
    }
}