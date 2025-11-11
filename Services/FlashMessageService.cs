using BlazorApp.Services.Interfaces;
using Microsoft.JSInterop;
using System.Timers;

namespace BlazorApp.Services
{
    public class FlashMessageService : IFlashMessage
    {
        public event Action? OnMessageChanged;
        public string? Message { get; private set; }
        public string CssClass { get; private set; } = "alert-info";

        public void ShowError(string message)
        {
            Message = message;
            CssClass = "alert-danger";
            Notify();
        }

        public void ShowSuccess(string message)
        {
            Message = message;
            CssClass = "alert-success";
            Notify();
        }

        public void ShowWarning(string message)
        {
            Message = message;
            CssClass = "alert-warning";
            Notify();
        }

        public void Clear()
        {
            Message = null;
            CssClass = string.Empty;
            OnMessageChanged?.Invoke();
        }

        private void Notify()
        {
            OnMessageChanged?.Invoke();
        }
    }
}
