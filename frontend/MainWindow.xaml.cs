using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//for api
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace _;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public class LoginResponse
{
    public int id { get; set; }
    public string? username { get; set; } // Make nullable
    public string? name { get; set; } // Make nullable
    public string? token { get; set; } // Make nullable
}

public class StudentResponse {
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<Student> Items { get; set; } = new List<Student>();
}

public class Student {
    public int Id { get; set; }
    public int RollNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public long Phone { get; set; }
    public string? Address { get; set; }
    public int CourseId { get; set; }
    public string? Photo { get; set; }
    public int Year { get; set; }
    public string? Gender { get; set; }
    public string? FatherName { get; set; }
    public string? CreatedOn { get; set; }
    public string? UpdatedOn { get; set; }
    public int TotalRecords { get; set; }
    public List<int> RecordIds { get; set; } = new List<int>();
    public Course Course { get; set; } = new Course();
}

public class Course {
    public int Id { get; set; }
    public string? Name { get; set; }
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter both username and password.", "Login Failed", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            // Show loading indicator or disable button here if needed
            LoginButton.IsEnabled = false;

            // Create the login request data
            var loginData = new
            {
                Username = username,
                Password = password
            };

            // Create HttpClient for API call
            using (HttpClient client = new HttpClient())
            {
                // Serialize the login data to JSON
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(loginData);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                // Make the API call
                HttpResponseMessage response = await client.PostAsync("http://localhost:5056/Librarian/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the response
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(responseBody);

                    // Store the token and user information
                    App.Current.Properties["AuthToken"] = loginResponse?.token;
                    App.Current.Properties["LibrarianId"] = loginResponse?.id;
                    App.Current.Properties["LibrarianName"] = loginResponse?.name;

                    // Update UI
                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.name)) {
                        LibrarianName.Text = loginResponse.name;
                        AuthToken.Text = loginResponse.token;
                        LoginPanel.Visibility = Visibility.Collapsed;
                        DashboardPanel.Visibility = Visibility.Visible;
                    } else {
                        MessageBox.Show("Login response was null.", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Handle failed login
                    MessageBox.Show($"Login failed. Status code: {response.StatusCode}", "Login Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            // Re-enable the login button
            LoginButton.IsEnabled = true;
        }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        // Clear login fields
        UsernameTextBox.Clear();
        PasswordBox.Clear();
        
        // Switch back to login view
        DashboardPanel.Visibility = Visibility.Collapsed;
        LoginPanel.Visibility = Visibility.Visible;
    }

    private void OpenStudents(object sender, RoutedEventArgs e)
    {
        // Original method kept for compatibility
        ShowStudentsPanel(sender, e);
    }

    // Navigation methods for tab switching
    private void ShowStudentsPanel(object sender, RoutedEventArgs e)
    {
        HideAllContentPanels();
        StudentsPanel.Visibility = Visibility.Visible;
        LoadStudentsData().ConfigureAwait(false);
    }
    
    private void ShowBooksPanel(object sender, RoutedEventArgs e)
    {
        HideAllContentPanels();
        BooksPanel.Visibility = Visibility.Visible;
        // TODO: Load books data when implemented
    }
    
    private void ShowCoursesPanel(object sender, RoutedEventArgs e)
    {
        HideAllContentPanels();
        CoursesPanel.Visibility = Visibility.Visible;
        // TODO: Load courses data when implemented
    }
    
    private void ShowRecordsPanel(object sender, RoutedEventArgs e)
    {
        HideAllContentPanels();
        RecordsPanel.Visibility = Visibility.Visible;
        // TODO: Load records data when implemented
    }
    
    private void HideAllContentPanels()
    {
        WelcomePanel.Visibility = Visibility.Collapsed;
        StudentsPanel.Visibility = Visibility.Collapsed;
        BooksPanel.Visibility = Visibility.Collapsed;
        CoursesPanel.Visibility = Visibility.Collapsed;
        RecordsPanel.Visibility = Visibility.Collapsed;
    }
    
    private async Task LoadStudentsData()
    {
        try 
        {
            var token = Application.Current.Properties["AuthToken"] as string ?? string.Empty;
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("http://localhost:5056/Students?page=1&pageSize=10&sortBy=created_on&orderBy=desc");
            
            if (response.IsSuccessStatusCode) 
            {
                var json = await response.Content.ReadAsStringAsync();
                var studentResponse = JsonSerializer.Deserialize<StudentResponse>(json);
                StudentsGrid.ItemsSource = studentResponse?.Items;
            } 
            else 
            {
                MessageBox.Show("Failed to retrieve student data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 
        catch (System.Exception ex) 
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    // Open the dedicated student management window
    private void OpenStudentManagement(object sender, RoutedEventArgs e)
    {
        var sm = new StudentManagement();
        sm.ShowDialog();
    }
}