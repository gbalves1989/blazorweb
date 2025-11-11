namespace BlazorApp.Services.Interfaces
{
    public interface IFlashMessage
    {
        string? Message { get; }
        string CssClass { get; }

        event Action? OnMessageChanged;

        void ShowSuccess(string message);
        void ShowError(string message);
        void ShowWarning(string message);
        void Clear();
    }
}
