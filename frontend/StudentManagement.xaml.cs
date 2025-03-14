using System;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace _ {
  public partial class StudentManagement : Window {
    public StudentManagement() {
      // Initialize the XAML components
      InitializeComponent();
      // Load students data
      LoadStudents().ConfigureAwait(false);
    }

    private async Task LoadStudents() {
      try {
        // Fix CS8602 warning - add null check
        var token = Application.Current.Properties["AuthToken"] as string ?? string.Empty;
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("http://localhost:5056/Students?page=1&pageSize=10&sortBy=created_on&orderBy=desc");
        if (response.IsSuccessStatusCode) {
          var json = await response.Content.ReadAsStringAsync();
          var studentResponse = JsonSerializer.Deserialize<StudentResponse>(json);
          StudentsGrid.ItemsSource = studentResponse?.Items;
        } else {
          MessageBox.Show("Failed to retrieve student data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      } catch (System.Exception ex) {
        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e) {
      // TODO: Implement add student functionality
      MessageBox.Show("Add student functionality to be implemented.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e) {
      var selectedStudent = StudentsGrid.SelectedItem as Student;
      if (selectedStudent != null) {
        // TODO: Implement update functionality
        MessageBox.Show($"Update student: {selectedStudent.Name}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
      } else {
        MessageBox.Show("Please select a student to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
      var selectedStudent = StudentsGrid.SelectedItem as Student;
      if (selectedStudent != null) {
        // TODO: Implement delete functionality
        MessageBox.Show($"Delete student: {selectedStudent.Name}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
      } else {
        MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
    }
  }
}
