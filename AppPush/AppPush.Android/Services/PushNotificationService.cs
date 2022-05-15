using Android.App;
using Android.Content;
using Android.Media;
using AndroidX.Core.App;
using AppPush.Logics;
using AppPush.Logics.Services.Interfaces;
using System.Threading.Tasks;

namespace AppPush.Droid.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private Context _context;
        private NotificationCompat.Builder _builder;
        public static string NotificationChannelId = "10023";

        public PushNotificationService()
        {
            _context = global::Android.App.Application.Context;
        }

        public void CreateNotification(string title, string message)
        {
            var intent = new Intent(_context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            intent.PutExtra(title, message);
            intent.PutExtra(Keys.IsPush, Keys.IsPush);
            var pendingIntent = PendingIntent.GetActivity(_context, 0, intent, PendingIntentFlags.OneShot);

            // Creating an Audio Attribute
            var alarmAttributes = new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Sonification)
                .SetUsage(AudioUsageKind.Notification).Build();

            _builder = new NotificationCompat.Builder(_context, NotificationChannelId);
            // set the notification icon, change whatever you want
            _builder.SetSmallIcon(Resource.Drawable.ic_mtrl_checked_circle);
            _builder.SetContentTitle(title)
                    .SetAutoCancel(true)
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetChannelId(NotificationChannelId)
                    .SetPriority((int)NotificationPriority.Default)
                    .SetVibrate(new long[0])
                    .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                    .SetVisibility((int)NotificationVisibility.Public)
                    .SetSmallIcon(Resource.Drawable.ic_mtrl_checked_circle)
                    .SetContentIntent(pendingIntent);

            var notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;

            if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
            {
                NotificationImportance importance = global::Android.App.NotificationImportance.High;

                NotificationChannel notificationChannel = new NotificationChannel(NotificationChannelId, title, importance);
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                if (notificationManager != null)
                {
                    _builder.SetChannelId(NotificationChannelId);
                    notificationManager.CreateNotificationChannel(notificationChannel);
                }
            }

            notificationManager.Notify(0, _builder.Build());
        }

        public Task<PermissionResult> GetGrantedState()
        {
            return Task.FromResult(new PermissionResult() { IsGranted = true });
        }

        public void RegisterRemoteNotification()
        {
            
        }

        public Task<string> GetTokenAsync()
        {
            //var task = TaskUtilities.ToAwaitableTask(FirebaseMessaging.Instance.GetToken());
            //var oToken = await task;
            //var token = oToken.ToString();
            //Debug.WriteLine($"fcm token: {token}");
            //return token;
            return Task.FromResult("");
        }
    }
}