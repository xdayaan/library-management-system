using System;
using System.Windows;
using LibraryManagementSystem.ViewModels;

namespace LibraryManagementSystem.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            Console.WriteLine("[MAINWINDOW] Constructor called");
            Console.WriteLine($"[MAINWINDOW] ViewModel received: {viewModel != null}");
            
            InitializeComponent();
            Console.WriteLine("[MAINWINDOW] InitializeComponent completed");
            
            DataContext = viewModel;
            Console.WriteLine("[MAINWINDOW] DataContext set");
            Console.WriteLine($"[MAINWINDOW] Current ViewModel in DataContext: {DataContext?.GetType().Name}");
            
            if (viewModel != null)
            {
                Console.WriteLine($"[MAINWINDOW] MainViewModel.CurrentViewModel: {viewModel.CurrentViewModel?.GetType().Name}");
            }
        }
    }
}
