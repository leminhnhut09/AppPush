using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPush.Logics.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task<PermissionResult> GetGrantedState();

        // Create notification, android
        void CreateNotification(string title, string message);

        // ios
        void RegisterRemoteNotification();

        Task<string> GetTokenAsync();
    }
    public class PermissionResult
    {
        public bool IsGranted { get; set; }
        public string Message { get; set; }
    }
}
