using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        protected readonly ApiService _apiService;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private string _successMessage = string.Empty;

        public BaseViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        private void ClearMessages()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
        }

        protected void SetError(string message)
        {
            ErrorMessage = message;
            SuccessMessage = string.Empty;
        }

        protected void SetSuccess(string message)
        {
            SuccessMessage = message;
            ErrorMessage = string.Empty;
        }

        protected void ClearAllMessages()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
        }
    }
}
