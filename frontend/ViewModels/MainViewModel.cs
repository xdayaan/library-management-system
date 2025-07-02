using System;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Views;

namespace LibraryManagementSystem.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        private object? _currentViewModel;

        [ObservableProperty]
        private string _currentUser = "Librarian";

        private readonly DashboardViewModel? _dashboardViewModel;
        private readonly BooksViewModel? _booksViewModel;
        private readonly StudentsViewModel? _studentsViewModel;
        private readonly RecordsViewModel? _recordsViewModel;
        private readonly CoursesViewModel? _coursesViewModel;

        public MainViewModel(ApiService apiService,
            DashboardViewModel? dashboardViewModel,
            BooksViewModel? booksViewModel,
            StudentsViewModel? studentsViewModel,
            RecordsViewModel? recordsViewModel,
            CoursesViewModel? coursesViewModel) : base(apiService)
        {
            Console.WriteLine("[MAINVIEWMODEL] Constructor called");
            Console.WriteLine($"[MAINVIEWMODEL] DashboardViewModel: {dashboardViewModel != null}");
            Console.WriteLine($"[MAINVIEWMODEL] BooksViewModel: {booksViewModel != null}");
            Console.WriteLine($"[MAINVIEWMODEL] StudentsViewModel: {studentsViewModel != null}");
            Console.WriteLine($"[MAINVIEWMODEL] RecordsViewModel: {recordsViewModel != null}");
            Console.WriteLine($"[MAINVIEWMODEL] CoursesViewModel: {coursesViewModel != null}");
            
            _dashboardViewModel = dashboardViewModel;
            _booksViewModel = booksViewModel;
            _studentsViewModel = studentsViewModel;
            _recordsViewModel = recordsViewModel;
            _coursesViewModel = coursesViewModel;

            Console.WriteLine("[MAINVIEWMODEL] Setting initial view to dashboard...");
            CurrentViewModel = _dashboardViewModel;
            Console.WriteLine($"[MAINVIEWMODEL] CurrentViewModel set to: {CurrentViewModel?.GetType().Name}");
            
            // Debug: Verify the dashboard is set
            System.Diagnostics.Debug.WriteLine($"MainViewModel initialized - CurrentViewModel is: {CurrentViewModel?.GetType().Name}");
        }

        [RelayCommand]
        private void NavigateToDashboard()
        {
            CurrentViewModel = _dashboardViewModel;
        }

        [RelayCommand]
        private void NavigateToBooks()
        {
            CurrentViewModel = _booksViewModel;
            _booksViewModel?.LoadBooksCommand.Execute(null);
        }

        [RelayCommand]
        private void NavigateToStudents()
        {
            CurrentViewModel = _studentsViewModel;
            _studentsViewModel?.LoadStudentsCommand.Execute(null);
        }

        [RelayCommand]
        private void NavigateToRecords()
        {
            CurrentViewModel = _recordsViewModel;
            _recordsViewModel?.LoadRecordsCommand.Execute(null);
        }

        [RelayCommand]
        private void NavigateToCourses()
        {
            CurrentViewModel = _coursesViewModel;
            _coursesViewModel?.LoadCoursesCommand.Execute(null);
        }

        [RelayCommand]
        private void Logout()
        {
            _apiService.ClearAuthToken();
            
            var loginWindow = App.GetService<LoginWindow>();
            loginWindow?.Show();

            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        }

        [RelayCommand]
        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
