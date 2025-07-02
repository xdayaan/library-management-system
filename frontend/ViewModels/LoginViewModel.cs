using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Views;

namespace LibraryManagementSystem.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private bool _rememberMe;

        public LoginViewModel(ApiService apiService) : base(apiService)
        {
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            Console.WriteLine("[LOGIN] Starting login process...");
            
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Console.WriteLine("[LOGIN] Validation failed: Missing username or password");
                SetError("Please enter both username and password.");
                return;
            }

            Console.WriteLine($"[LOGIN] Attempting to login user: {Username}");
            IsLoading = true;
            ClearAllMessages();

            try
            {
                var loginDto = new LoginDto
                {
                    Username = Username,
                    Password = Password
                };

                Console.WriteLine("[LOGIN] Calling API login...");
                var result = await _apiService.LoginAsync(loginDto);

                if (result.Success)
                {
                    Console.WriteLine("[LOGIN] API login successful, setting auth token...");
                    _apiService.SetAuthToken(result.Token);
                    
                    try
                    {
                        Console.WriteLine("[LOGIN] Getting login window reference...");
                        var loginWindow = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                        Console.WriteLine($"[LOGIN] Login window found: {loginWindow != null}");
                        
                        Console.WriteLine("[LOGIN] Creating main window from DI container...");
                        var mainWindow = App.GetService<MainWindow>();
                        Console.WriteLine($"[LOGIN] Main window created: {mainWindow != null}");
                        
                        Console.WriteLine("[LOGIN] Showing main window...");
                        mainWindow?.Show();
                        
                        Console.WriteLine("[LOGIN] Activating and maximizing main window...");
                        if (mainWindow != null)
                        {
                            mainWindow.Activate();
                            mainWindow.WindowState = WindowState.Maximized;
                        }
                        
                        Console.WriteLine("[LOGIN] Closing login window...");
                        loginWindow?.Close();
                        
                        Console.WriteLine("[LOGIN] Login process completed successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[LOGIN] ERROR opening main window: {ex.Message}");
                        Console.WriteLine($"[LOGIN] Stack trace: {ex.StackTrace}");
                        SetError($"Failed to open main window: {ex.Message}");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"[LOGIN] API login failed: {result.Message}");
                    SetError(result.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOGIN] EXCEPTION during login: {ex.Message}");
                Console.WriteLine($"[LOGIN] Stack trace: {ex.StackTrace}");
                SetError($"Login failed: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                Console.WriteLine("[LOGIN] Login process finished");
            }
        }

        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
