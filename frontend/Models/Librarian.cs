using System;
using System.ComponentModel;

namespace LibraryManagementSystem.Models
{
    public class Librarian : INotifyPropertyChanged
    {
        private int _id;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _name = string.Empty;
        private DateTime _createdOn;
        private DateTime _updatedOn;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
