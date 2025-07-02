using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.ViewModels
{
    public partial class StudentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Student> _students = new();

        [ObservableProperty]
        private ObservableCollection<Course> _courses = new();

        [ObservableProperty]
        private Student? _selectedStudent;

        [ObservableProperty]
        private Student _newStudent = new();

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

        public StudentsViewModel(ApiService apiService) : base(apiService)
        {
            NewStudent = new Student
            {
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Year = DateTime.Now.Year
            };
        }

        [RelayCommand]
        private async Task LoadStudentsAsync()
        {
            IsLoading = true;
            try
            {
                var response = await _apiService.GetStudentsAsync(CurrentPage, PageSize, SearchText);
                Students.Clear();
                
                foreach (var student in response.Data)
                {
                    Students.Add(student);
                }

                TotalItems = response.TotalCount;
                TotalPages = response.TotalPages;
            }
            catch (Exception ex)
            {
                SetError($"Failed to load students: {ex.Message}");
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
        private async Task SearchStudentsAsync()
        {
            CurrentPage = 1;
            await LoadStudentsAsync();
        }

        [RelayCommand]
        private void OpenAddDialog()
        {
            IsEditMode = false;
            NewStudent = new Student
            {
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Year = DateTime.Now.Year
            };
            IsAddEditDialogOpen = true;
            LoadCoursesCommand.Execute(null);
        }

        [RelayCommand]
        private void OpenEditDialog(Student? student)
        {
            if (student == null) return;
            
            IsEditMode = true;
            NewStudent = new Student
            {
                Id = student.Id,
                RollNumber = student.RollNumber,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                CourseId = student.CourseId,
                Photo = student.Photo,
                Year = student.Year,
                Gender = student.Gender,
                FatherName = student.FatherName,
                Password = student.Password
            };
            IsAddEditDialogOpen = true;
            LoadCoursesCommand.Execute(null);
        }

        [RelayCommand]
        private async Task SaveStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(NewStudent.Name) || 
                string.IsNullOrWhiteSpace(NewStudent.Email) ||
                NewStudent.CourseId == 0)
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
                    NewStudent.UpdatedOn = DateTime.Now;
                    success = await _apiService.UpdateStudentAsync(NewStudent);
                    if (success)
                    {
                        SetSuccess("Student updated successfully!");
                    }
                }
                else
                {
                    NewStudent.CreatedOn = DateTime.Now;
                    NewStudent.UpdatedOn = DateTime.Now;
                    success = await _apiService.CreateStudentAsync(NewStudent);
                    if (success)
                    {
                        SetSuccess("Student added successfully!");
                    }
                }

                if (success)
                {
                    IsAddEditDialogOpen = false;
                    await LoadStudentsAsync();
                }
                else
                {
                    SetError("Failed to save student.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to save student: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task DeleteStudentAsync(Student? student)
        {
            if (student == null) return;

            IsLoading = true;
            try
            {
                var success = await _apiService.DeleteStudentAsync(student.Id);
                if (success)
                {
                    SetSuccess("Student deleted successfully!");
                    await LoadStudentsAsync();
                }
                else
                {
                    SetError("Failed to delete student.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to delete student: {ex.Message}");
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
                await LoadStudentsAsync();
            }
        }

        [RelayCommand]
        private async Task PreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadStudentsAsync();
            }
        }

        [RelayCommand]
        private async Task GoToPageAsync(int page)
        {
            if (page >= 1 && page <= TotalPages && page != CurrentPage)
            {
                CurrentPage = page;
                await LoadStudentsAsync();
            }
        }
    }
}
