using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int _totalBooks;

        [ObservableProperty]
        private int _totalStudents;

        [ObservableProperty]
        private int _totalIssuedBooks;

        [ObservableProperty]
        private int _totalOverdueBooks;

        [ObservableProperty]
        private ObservableCollection<DashboardCard> _dashboardCards = new();

        [ObservableProperty]
        private ObservableCollection<RecentActivity> _recentActivities = new();

        public DashboardViewModel(ApiService apiService) : base(apiService)
        {
            Console.WriteLine("[DASHBOARDVIEWMODEL] Constructor called");
            LoadDashboardData();
            Console.WriteLine("[DASHBOARDVIEWMODEL] LoadDashboardData called");
        }

        private void LoadDashboardData()
        {
            // Sample dashboard data - replace with actual API calls
            TotalBooks = 1250;
            TotalStudents = 340;
            TotalIssuedBooks = 89;
            TotalOverdueBooks = 12;

            DashboardCards.Clear();
            DashboardCards.Add(new DashboardCard { Title = "Total Books", Value = TotalBooks.ToString(), Icon = "📚", Color = "#0E8C91" });
            DashboardCards.Add(new DashboardCard { Title = "Total Students", Value = TotalStudents.ToString(), Icon = "👥", Color = "#88E7D8" });
            DashboardCards.Add(new DashboardCard { Title = "Issued Books", Value = TotalIssuedBooks.ToString(), Icon = "📖", Color = "#28a745" });
            DashboardCards.Add(new DashboardCard { Title = "Overdue Books", Value = TotalOverdueBooks.ToString(), Icon = "⚠️", Color = "#dc3545" });

            // Sample recent activities
            RecentActivities.Clear();
            RecentActivities.Add(new RecentActivity { Activity = "Book issued to John Doe", Time = "2 minutes ago", Type = "Issue" });
            RecentActivities.Add(new RecentActivity { Activity = "Book returned by Jane Smith", Time = "15 minutes ago", Type = "Return" });
            RecentActivities.Add(new RecentActivity { Activity = "New student registered: Mike Johnson", Time = "1 hour ago", Type = "Registration" });
            RecentActivities.Add(new RecentActivity { Activity = "Book added: Advanced Mathematics", Time = "2 hours ago", Type = "Addition" });
        }

        [RelayCommand]
        private void RefreshDashboard()
        {
            IsLoading = true;
            try
            {
                // Refresh dashboard data
                LoadDashboardData();
                SetSuccess("Dashboard refreshed successfully!");
            }
            catch (Exception ex)
            {
                SetError($"Failed to refresh dashboard: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    public class DashboardCard
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    public class RecentActivity
    {
        public string Activity { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
