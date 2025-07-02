using System;
using System.ComponentModel;

namespace LibraryManagementSystem.Models
{
    public enum Status
    {
        Active = 1,
        Inactive = 0
    }

    public class Book : INotifyPropertyChanged
    {
        private int _id;
        private int _serialNumber;
        private string _name = string.Empty;
        private string _author = string.Empty;
        private string _publisher = string.Empty;
        private string _subject = string.Empty;
        private int _courseId;
        private DateTime _publishedDate;
        private int _stock;
        private Status _status;
        private string _isbn = string.Empty;
        private int _edition;
        private int _pages;
        private string _rackNumber = string.Empty;
        private DateTime _createdOn;
        private DateTime _updatedOn;
        private Course? _course;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int SerialNumber
        {
            get => _serialNumber;
            set { _serialNumber = value; OnPropertyChanged(nameof(SerialNumber)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Author
        {
            get => _author;
            set { _author = value; OnPropertyChanged(nameof(Author)); }
        }

        public string Publisher
        {
            get => _publisher;
            set { _publisher = value; OnPropertyChanged(nameof(Publisher)); }
        }

        public string Subject
        {
            get => _subject;
            set { _subject = value; OnPropertyChanged(nameof(Subject)); }
        }

        public int CourseId
        {
            get => _courseId;
            set { _courseId = value; OnPropertyChanged(nameof(CourseId)); }
        }

        public DateTime PublishedDate
        {
            get => _publishedDate;
            set { _publishedDate = value; OnPropertyChanged(nameof(PublishedDate)); }
        }

        public int Stock
        {
            get => _stock;
            set { _stock = value; OnPropertyChanged(nameof(Stock)); }
        }

        public Status Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); OnPropertyChanged(nameof(StatusText)); }
        }

        public string StatusText => Status == Status.Active ? "Active" : "Inactive";

        public string ISBN
        {
            get => _isbn;
            set { _isbn = value; OnPropertyChanged(nameof(ISBN)); }
        }

        public int Edition
        {
            get => _edition;
            set { _edition = value; OnPropertyChanged(nameof(Edition)); }
        }

        public int Pages
        {
            get => _pages;
            set { _pages = value; OnPropertyChanged(nameof(Pages)); }
        }

        public string RackNumber
        {
            get => _rackNumber;
            set { _rackNumber = value; OnPropertyChanged(nameof(RackNumber)); }
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
