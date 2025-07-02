using System;
using System.ComponentModel;

namespace LibraryManagementSystem.Models
{
    public class Course : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private DateTime _createdOn;
        private DateTime _updatedOn;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
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
