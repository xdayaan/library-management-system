using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.ViewModels
{
    public partial class RecordsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Record> _records = new();

        [ObservableProperty]
        private ObservableCollection<Book> _books = new();

        [ObservableProperty]
        private ObservableCollection<Student> _students = new();

        [ObservableProperty]
        private Record? _selectedRecord;

        [ObservableProperty]
        private int _selectedBookId;

        [ObservableProperty]
        private int _selectedStudentId;

        [ObservableProperty]
        private DateTime _dueDate = DateTime.Now.AddDays(14);

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _totalPages = 1;

        [ObservableProperty]
        private int _pageSize = 10;

        [ObservableProperty]
        private int _totalItems;

        [ObservableProperty]
        private bool _isIssueDialogOpen;

        public RecordsViewModel(ApiService apiService) : base(apiService)
        {
        }

        [RelayCommand]
        private async Task LoadRecordsAsync()
        {
            IsLoading = true;
            try
            {
                var response = await _apiService.GetRecordsAsync(CurrentPage, PageSize);
                Records.Clear();
                
                foreach (var record in response.Data)
                {
                    Records.Add(record);
                }

                TotalItems = response.TotalCount;
                TotalPages = response.TotalPages;
            }
            catch (Exception ex)
            {
                SetError($"Failed to load records: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task LoadBooksAsync()
        {
            try
            {
                var response = await _apiService.GetBooksAsync(1, 1000); // Get all books for dropdown
                Books.Clear();
                
                foreach (var book in response.Data.Where(b => b.Status == Status.Active && b.Stock > 0))
                {
                    Books.Add(book);
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to load books: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task LoadStudentsAsync()
        {
            try
            {
                var response = await _apiService.GetStudentsAsync(1, 1000); // Get all students for dropdown
                Students.Clear();
                
                foreach (var student in response.Data)
                {
                    Students.Add(student);
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to load students: {ex.Message}");
            }
        }

        [RelayCommand]
        private void OpenIssueDialog()
        {
            SelectedBookId = 0;
            SelectedStudentId = 0;
            DueDate = DateTime.Now.AddDays(14);
            IsIssueDialogOpen = true;
            LoadBooksCommand.Execute(null);
            LoadStudentsCommand.Execute(null);
        }

        [RelayCommand]
        private async Task IssueBookAsync()
        {
            if (SelectedBookId == 0 || SelectedStudentId == 0)
            {
                SetError("Please select both a book and a student.");
                return;
            }

            if (DueDate <= DateTime.Now)
            {
                SetError("Due date must be in the future.");
                return;
            }

            IsLoading = true;
            try
            {
                var success = await _apiService.IssueBookAsync(SelectedBookId, SelectedStudentId, DueDate);
                if (success)
                {
                    SetSuccess("Book issued successfully!");
                    IsIssueDialogOpen = false;
                    await LoadRecordsAsync();
                }
                else
                {
                    SetError("Failed to issue book.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to issue book: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task ReturnBookAsync(Record? record)
        {
            if (record == null || record.IsReturned) return;

            IsLoading = true;
            try
            {
                var success = await _apiService.ReturnBookAsync(record.Id);
                if (success)
                {
                    SetSuccess("Book returned successfully!");
                    await LoadRecordsAsync();
                }
                else
                {
                    SetError("Failed to return book.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to return book: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void CancelIssueDialog()
        {
            IsIssueDialogOpen = false;
        }

        [RelayCommand]
        private async Task NextPageAsync()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await LoadRecordsAsync();
            }
        }

        [RelayCommand]
        private async Task PreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadRecordsAsync();
            }
        }

        [RelayCommand]
        private async Task GoToPageAsync(int page)
        {
            if (page >= 1 && page <= TotalPages && page != CurrentPage)
            {
                CurrentPage = page;
                await LoadRecordsAsync();
            }
        }
    }
}
