using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.ViewModels
{
    public partial class CoursesViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Course> _courses = new();

        [ObservableProperty]
        private Course? _selectedCourse;

        [ObservableProperty]
        private Course _newCourse = new();

        [ObservableProperty]
        private bool _isAddEditDialogOpen;

        [ObservableProperty]
        private bool _isEditMode;

        public CoursesViewModel(ApiService apiService) : base(apiService)
        {
            NewCourse = new Course
            {
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
        }

        [RelayCommand]
        private async Task LoadCoursesAsync()
        {
            IsLoading = true;
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
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void OpenAddDialog()
        {
            IsEditMode = false;
            NewCourse = new Course
            {
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            IsAddEditDialogOpen = true;
        }

        [RelayCommand]
        private void OpenEditDialog(Course? course)
        {
            if (course == null) return;
            
            IsEditMode = true;
            NewCourse = new Course
            {
                Id = course.Id,
                Name = course.Name,
                CreatedOn = course.CreatedOn,
                UpdatedOn = DateTime.Now
            };
            IsAddEditDialogOpen = true;
        }

        [RelayCommand]
        private async Task SaveCourseAsync()
        {
            if (string.IsNullOrWhiteSpace(NewCourse.Name))
            {
                SetError("Please enter a course name.");
                return;
            }

            IsLoading = true;
            try
            {
                bool success;
                if (IsEditMode)
                {
                    NewCourse.UpdatedOn = DateTime.Now;
                    // Note: Update method would need to be implemented in API service
                    success = false; // await _apiService.UpdateCourseAsync(NewCourse);
                    SetError("Course update functionality needs to be implemented in the backend.");
                }
                else
                {
                    NewCourse.CreatedOn = DateTime.Now;
                    NewCourse.UpdatedOn = DateTime.Now;
                    success = await _apiService.CreateCourseAsync(NewCourse);
                    if (success)
                    {
                        SetSuccess("Course added successfully!");
                    }
                }

                if (success)
                {
                    IsAddEditDialogOpen = false;
                    await LoadCoursesAsync();
                }
                else if (!IsEditMode)
                {
                    SetError("Failed to save course.");
                }
            }
            catch (Exception ex)
            {
                SetError($"Failed to save course: {ex.Message}");
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
    }
}
