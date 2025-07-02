using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.ViewModels;
using LibraryManagementSystem.Views;

namespace LibraryManagementSystem
{
    public partial class App : Application
    {
        private IHost? _host;

        public App()
        {
            // Handle unhandled exceptions
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled exception: {e.Exception.Message}\n\nStack Trace:\n{e.Exception.StackTrace}", 
                           "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            MessageBox.Show($"Fatal exception: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                           "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Console.WriteLine("[APP] Application startup initiated");
            
            try
            {
                Console.WriteLine("[APP] Building dependency injection host...");
                _host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        Console.WriteLine("[APP] Configuring services...");
                        
                        // Register HttpClient and ApiService
                        services.AddHttpClient<ApiService>();
                        Console.WriteLine("[APP] HttpClient and ApiService registered");
                        
                        // Register ViewModels
                        services.AddTransient<MainViewModel>();
                        services.AddTransient<LoginViewModel>();
                        services.AddTransient<DashboardViewModel>();
                        services.AddTransient<BooksViewModel>();
                        services.AddTransient<StudentsViewModel>();
                        services.AddTransient<RecordsViewModel>();
                        services.AddTransient<CoursesViewModel>();
                        
                        // Register Views
                        services.AddTransient<MainWindow>();
                        services.AddTransient<LoginWindow>();
                    })
                    .Build();

                _host.Start();
                Console.WriteLine("[APP] Host started successfully");

                // Show login window
                Console.WriteLine("[APP] Creating login window...");
                var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
                Console.WriteLine($"[APP] Login window created: {loginWindow != null}");
                
                Console.WriteLine("[APP] Showing login window...");
                loginWindow?.Show();
                Console.WriteLine("[APP] Login window shown");

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application startup failed: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                               "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }

        public static T GetService<T>() where T : class
        {
            return ((App)Current)._host!.Services.GetRequiredService<T>();
        }
    }
}
