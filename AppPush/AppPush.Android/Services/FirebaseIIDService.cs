
using Android.App;
using Firebase.Messaging;
using System;
using System.Diagnostics;

namespace AppPush.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT", "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseIIDService : FirebaseMessagingService
    {
        public override void OnNewToken(string token)
        {
            Debug.WriteLine($"new token: {token}");
            SendRegistraionToServer(token);
            base.OnNewToken(token);
        }

        private void SendRegistraionToServer(string token)
        {
            //todo: send token to your app server
        }

        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);
            //var helper = Ioc.Current.Resolve<IPushNotificationService>();
            var notif = p0?.GetNotification();
            string notifTitle = notif?.Title;
            string notifBody = notif?.Body;
            //helper?.CreateNotification(notifTitle, notifBody);
            //helper?.CreateNotification(notifTitle, notifBody);
            new PushNotificationService().CreateNotification(notifTitle, notifBody);
        }
    }
}