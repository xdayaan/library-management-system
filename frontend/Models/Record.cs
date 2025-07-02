using System;
using System.ComponentModel;

namespace LibraryManagementSystem.Models
{
    public class Record : INotifyPropertyChanged
    {
        private int _id;
        private int _bookId;
        private int _studentId;
        private DateTime _issueDate;
        private DateTime? _returnDate;
        private DateTime _dueDate;
        private int _fine;
        private DateTime _createdOn;
        private DateTime _updatedOn;
        private Book? _book;
        private Student? _student;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int BookId
        {
            get => _bookId;
            set { _bookId = value; OnPropertyChanged(nameof(BookId)); }
        }

        public int StudentId
        {
            get => _studentId;
            set { _studentId = value; OnPropertyChanged(nameof(StudentId)); }
        }

        public DateTime IssueDate
        {
            get => _issueDate;
            set { _issueDate = value; OnPropertyChanged(nameof(IssueDate)); }
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set { _returnDate = value; OnPropertyChanged(nameof(ReturnDate)); OnPropertyChanged(nameof(IsReturned)); OnPropertyChanged(nameof(StatusText)); }
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set { _dueDate = value; OnPropertyChanged(nameof(DueDate)); OnPropertyChanged(nameof(IsOverdue)); }
        }

        public int Fine
        {
            get => _fine;
            set { _fine = value; OnPropertyChanged(nameof(Fine)); }
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

        public Book? Book
        {
            get => _book;
            set { _book = value; OnPropertyChanged(nameof(Book)); OnPropertyChanged(nameof(BookName)); }
        }

        public Student? Student
        {
            get => _student;
            set { _student = value; OnPropertyChanged(nameof(Student)); OnPropertyChanged(nameof(StudentName)); }
        }

        public string BookName => Book?.Name ?? "N/A";
        public string StudentName => Student?.Name ?? "N/A";
        public bool IsReturned => ReturnDate.HasValue;
        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
        public string StatusText => IsReturned ? "Returned" : (IsOverdue ? "Overdue" : "Issued");

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
