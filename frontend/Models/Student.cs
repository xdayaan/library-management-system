using System;
using System.ComponentModel;

namespace LibraryManagementSystem.Models
{
    public class Student : INotifyPropertyChanged
    {
        private int _id;
        private int _rollNumber;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private long _phone;
        private string _address = string.Empty;
        private int _courseId;
        private string _photo = string.Empty;
        private int _year;
        private string _password = string.Empty;
        private DateTime _createdOn;
        private DateTime _updatedOn;
        private string _gender = string.Empty;
        private string _fatherName = string.Empty;
        private Course? _course;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int RollNumber
        {
            get => _rollNumber;
            set { _rollNumber = value; OnPropertyChanged(nameof(RollNumber)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public long Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public int CourseId
        {
            get => _courseId;
            set { _courseId = value; OnPropertyChanged(nameof(CourseId)); }
        }

        public string Photo
        {
            get => _photo;
            set { _photo = value; OnPropertyChanged(nameof(Photo)); }
        }

        public int Year
        {
            get => _year;
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public DateTime CreatedOn
        {
            get => _createdOn;
            set { _createdOn = value; OnPropertyChanged(nameof(CreatedOn)); }
        }

        public DateTime UpdatedOn
        {
            get => _updatedOn;
            set { _updatedOn = value; OnPropertyChanged(nameof(UpdatedOn)); }
        }

        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        public string FatherName
        {
            get => _fatherName;
            set { _fatherName = value; OnPropertyChanged(nameof(FatherName)); }
        }

        public Course? Course
        {
            get => _course;
            set { _course = value; OnPropertyChanged(nameof(Course)); OnPropertyChanged(nameof(CourseName)); }
        }

        public string CourseName => Course?.Name ?? "N/A";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
