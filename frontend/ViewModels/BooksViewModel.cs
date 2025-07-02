using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.ViewModels
{
    public partial class BooksViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Book> _books = new();

        [ObservableProperty]
        private ObservableCollection<Course> _courses = new();

        [ObservableProperty]
        private Book? _selectedBook;

        [ObservableProperty]
        private Book _newBook = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _totalPages = 1;

        [ObservableProperty]
        private int _pageSize = 10;

        [ObservableProperty]
        private int _totalItems;

        [ObservableProperty]
        private bool _isAddEditDialogOpen;

        [ObservableProperty]
        private bool _isEditMode;

        [ObservableProperty]
        private ObservableCollection<int> _pageNumbers = new();

        public BooksViewModel(ApiService apiService) : base(apiService)
        {
            NewBook = new Book
            {
                Status = Status.Active,
                PublishedDate = DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
        }

        [RelayCommand]
        private async Task LoadBooksAsync()
        {
            IsLoading = true;
            try
            {
                var response = await _apiService.GetBooksAsync(CurrentPage, PageSize, SearchText);
                Books.Clear();
                
                foreach (var book in response.Data)
                {
                    Books.Add(book);
                }

                TotalItems = response.TotalCount;
                TotalPages = response.TotalPages;
                UpdatePageNumbers();
                UpdatePageNumbers();
            }
            catch (Exception ex)
            {
                SetError($"Failed to load books: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task LoadCoursesAsync()
        {
            try
            {
                var courses = await _apiService.GetCoursesAsync();
                Courses.Clear();
                
                foreach (var course in courses)
                {
                    Courses.Add(course);
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to load courses: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SearchBooksAsync()
        {
            CurrentPage = 1;
            await LoadBooksAsync();
        }

        [RelayCommand]
        private void OpenAddDialog()
        {
            IsEditMode = false;
            NewBook = new Book
            {
                Status = Status.Active,
                PublishedDate = DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            IsAddEditDialogOpen = true;
            LoadCoursesCommand.Execute(null);
        }

        [RelayCommand]
        private void OpenEditDialog(Book? book)
        {
            if (book == null) return;
            
            IsEditMode = true;
            NewBook = new Book
            {
                Id = book.Id,
                SerialNumber = book.SerialNumber,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Subject = book.Subject,
                CourseId = book.CourseId,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock,
                Status = book.Status,
                ISBN = book.ISBN,
                Edition = book.Edition,
                Pages = book.Pages,
                RackNumber = book.RackNumber
            };
            IsAddEditDialogOpen = true;
            LoadCoursesCommand.Execute(null);
        }

        [RelayCommand]
        private async Task SaveBookAsync()
        {
            if (string.IsNullOrWhiteSpace(NewBook.Name) || 
                string.IsNullOrWhiteSpace(NewBook.Author) ||
                NewBook.CourseId == 0)
            {
                SetError("Please fill in all required fields.");
                return;
            }

            IsLoading = true;
            try
            {
                bool success;
                if (IsEditMode)
                {
                    NewBook.UpdatedOn = DateTime.Now;
                    success = await _apiService.UpdateBookAsync(NewBook);
                    if (success)
                    {
                        SetSuccess("Book updated successfully!");
                    }
                }
                else
                {
                    NewBook.CreatedOn = DateTime.Now;
                    NewBook.UpdatedOn = DateTime.Now;
                    success = await _apiService.CreateBookAsync(NewBook);
                    if (success)
                    {
                        SetSuccess("Book added successfully!");
                    }
                }

                if (success)
                {
                    IsAddEditDialogOpen = false;
                    await LoadBooksAsync();
                }
                else
                {
                    SetError("Failed to save book.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to save book: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task DeleteBookAsync(Book? book)
        {
            if (book == null) return;

            IsLoading = true;
            try
            {
                var success = await _apiService.DeleteBookAsync(book.Id);
                if (success)
                {
                    SetSuccess("Book deleted successfully!");
                    await LoadBooksAsync();
                }
                else
                {
                    SetError("Failed to delete book.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to delete book: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void CancelDialog()
        {
            IsAddEditDialogOpen = false;
        }

        [RelayCommand]
        private async Task NextPageAsync()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await LoadBooksAsync();
            }
        }

        [RelayCommand]
        private async Task PreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadBooksAsync();
            }
        }

        [RelayCommand]
        private async Task GoToPageAsync(int page)
        {
            if (page >= 1 && page <= TotalPages && page != CurrentPage)
            {
                CurrentPage = page;
                await LoadBooksAsync();
            }
        }

        private void UpdatePageNumbers()
        {
            PageNumbers.Clear();
            
            int startPage = Math.Max(1, CurrentPage - 2);
            int endPage = Math.Min(TotalPages, CurrentPage + 2);
            
            for (int i = startPage; i <= endPage; i++)
            {
                PageNumbers.Add(i);
            }
        }
    }
}
