using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using Newtonsoft.Json;

namespace LibraryManagementSystem.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string? _token;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5056"; // Update with your backend URL
        }

        public void SetAuthToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }

        public void ClearAuthToken()
        {
            _token = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Authentication
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                Console.WriteLine($"[APISERVICE] Attempting login for user: {loginDto.Username}");
                Console.WriteLine($"[APISERVICE] Login URL: {_baseUrl}/librarian/login");
                
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/librarian/login", loginDto);
                var content = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"[APISERVICE] Response status: {response.StatusCode}");
                Console.WriteLine($"[APISERVICE] Response content: {content}");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<LoginResponseDto>(content);
                    Console.WriteLine($"[APISERVICE] Login successful: {result?.Success}");
                    Console.WriteLine($"[APISERVICE] Token received: {!string.IsNullOrEmpty(result?.Token)}");
                    return result ?? new LoginResponseDto { Success = false, Message = "Invalid response" };
                }
                
                Console.WriteLine($"[APISERVICE] Login failed with status: {response.StatusCode}");
                return new LoginResponseDto { Success = false, Message = $"Login failed: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[APISERVICE] Exception during login: {ex.Message}");
                Console.WriteLine($"[APISERVICE] Stack trace: {ex.StackTrace}");
                return new LoginResponseDto { Success = false, Message = $"Login error: {ex.Message}" };
            }
        }

        // Books API
        public async Task<PagedResponse<Book>> GetBooksAsync(int page = 1, int pageSize = 10, string? search = null)
        {
            try
            {
                var queryParams = new List<string>
                {
                    $"page={page}",
                    $"pageSize={pageSize}"
                };

                if (!string.IsNullOrEmpty(search))
                {
                    queryParams.Add($"name={Uri.EscapeDataString(search)}");
                }

                var query = string.Join("&", queryParams);
                var response = await _httpClient.GetAsync($"{_baseUrl}/books?{query}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PagedResponse<Book>>(content);
                    return result ?? new PagedResponse<Book>();
                }
                
                return new PagedResponse<Book>();
            }
            catch (Exception)
            {
                return new PagedResponse<Book>();
            }
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/books/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Book>(content);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/books", book);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/books/{book.Id}", book);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/books/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Students API
        public async Task<PagedResponse<Student>> GetStudentsAsync(int page = 1, int pageSize = 10, string? search = null)
        {
            try
            {
                var queryParams = new List<string>
                {
                    $"page={page}",
                    $"pageSize={pageSize}"
                };

                if (!string.IsNullOrEmpty(search))
                {
                    queryParams.Add($"name={Uri.EscapeDataString(search)}");
                }

                var query = string.Join("&", queryParams);
                var response = await _httpClient.GetAsync($"{_baseUrl}/students?{query}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PagedResponse<Student>>(content);
                    return result ?? new PagedResponse<Student>();
                }
                
                return new PagedResponse<Student>();
            }
            catch (Exception)
            {
                return new PagedResponse<Student>();
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/students/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Student>(content);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateStudentAsync(Student student)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/students", student);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/students/{student.Id}", student);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/students/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Courses API
        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/courses");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Course>>(content);
                    return result ?? new List<Course>();
                }
                return new List<Course>();
            }
            catch (Exception)
            {
                return new List<Course>();
            }
        }

        public async Task<bool> CreateCourseAsync(Course course)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/courses", course);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Records API
        public async Task<PagedResponse<Record>> GetRecordsAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/record?page={page}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PagedResponse<Record>>(content);
                    return result ?? new PagedResponse<Record>();
                }
                return new PagedResponse<Record>();
            }
            catch (Exception)
            {
                return new PagedResponse<Record>();
            }
        }

        public async Task<bool> IssueBookAsync(int bookId, int studentId, DateTime dueDate)
        {
            try
            {
                var record = new
                {
                    BookId = bookId,
                    StudentId = studentId,
                    IssueDate = DateTime.Now,
                    DueDate = dueDate,
                    Fine = 0
                };

                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/record", record);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ReturnBookAsync(int recordId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"{_baseUrl}/record/{recordId}/return", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
