   M S B A M L   `   `   `  ����R  NLibraryManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:LibraryManagementSystem.ViewModels"LibraryManagementSystem.ViewModels  5   	   M+clr-namespace:LibraryManagementSystem.ViewsLibraryManagementSystem.Views  5   	   .    (LibraryManagementSystem.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   A
viewmodels0clr-namespace:LibraryManagementSystem.ViewModels  5   	   7views+clr-namespace:LibraryManagementSystem.Views  5   	     � Title$  Library Management System��5   	   $	��800��5   	   $
��1400��6    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�5
   	    � DataContext 5      6   0LibraryManagementSystem.ViewModels.MainViewModel  5   
    � 	Resources 5       �� DataType;   5LibraryManagementSystem.ViewModels.DashboardViewModel1   +LibraryManagementSystem.Views.DashboardView7   1LibraryManagementSystem.ViewModels.BooksViewModel-   'LibraryManagementSystem.Views.BooksView:   4LibraryManagementSystem.ViewModels.StudentsViewModel0   *LibraryManagementSystem.Views.StudentsView9   3LibraryManagementSystem.ViewModels.RecordsViewModel/	   )LibraryManagementSystem.Views.RecordsView9
   3LibraryManagementSystem.ViewModels.CoursesViewModel/   )LibraryManagementSystem.Views.CoursesView��%w  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)(�� �     5      *,
 +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5      	  5      �� 5   
   " 
 6   .R�5        5       5!      .��5#      �  � RowDefinitions 5$   
   �� 5%      $
��Auto �6   �� 5&      $��* �6   .+�5*   
   �� $��0��6      HeaderGradientBrush#��[  6   $��80��6P   ._�5+      � $
��24,0q�6    � ColumnDefinitions 5,      �� 5-      $
��Auto �6'   �� 5.      $��* �6'   �� 5/      $
��Auto �6'   .+�53      �� $��0��6    �� Orientation$ 
Horizontal=�6-   $��Center=�6F   .+�54      '  !MaterialDesignThemes.Wpf.PackIcon  	   Kind$	 LibraryShelves=�6.   $��40��55   ,   $��40��67     LightAquaBrush#��[ 56   ,   $��0,0,16,0q�57   ,   �� 59      $��Center=�6!   .+�5:      �� $��Library Management System��6$   $��20(�5;   $   $
��Bold"�62     
WhiteBrush#��[ 5<   $   �� 5=      $��Professional Edition��6$   $��12(�5>   $   #��[ 5?   $   5@      5A      �� 5D      $��2��6   $ 
Horizontal=�6-   $��Center=�6F   .+�5E      �� $��14(�5F       $��Medium"�6.   #��[ 5G       $��Center=�5H       $��0,0,16,0q�5I       ��5E       �� *CurrentUser+5I       �� 5K        MaterialDesignIconButton#��[ 6   
 � ToolTip$
 Logout��5M      $	��8,0q�5N      ��5L      �� *LogoutCommand+.��5O        $	 Logout=�62   $��24��5P   0   $��24��6;   #��[ 5Q   0   5R      �� 5T      #��[ 6   $
 Exit Application��5V      $	��8,0q�5W      ��5U      �� *ExitCommand+.��5X        $	 	ExitToApp=�62   $��24��5Y   0   $��24��6;   #��[ 5Z   0   5[      5\      5]      5^      � 5a   
   $��1��6    5b      �� 5c      $	��280 �6#   �� 5d      $��* �6#   .+�5h      �� $��0��6     	NavyBrush#��[ 6%   ._�5i      �� $��0,24,0,0q�6   .+�5k      ��   MaterialDesignFlatButton#��[ 6   $��56��5m      $��Stretch=�6)   $
��Left=�5n      #��[ 5o      $	��8,4q�5p      ��5l      �� *NavigateToDashboardCommand+.��5q      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5r        $	 ViewDashboard=�66   $��24��5s   4   $��24��6?   $��0,0,16,0q�5t   4   �� 5u      $��	Dashboard��6(   $��16(�69   $��Center=�6G   5v      5w      �� 5y      #��[ 6   $��56��5{      $��Stretch=�6)   $
��Left=�5|      #��[ 5}      $	��8,4q�5~      ��5z      �� *NavigateToBooksCommand+.��5      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $
	 Book=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Books��6(   $��16(�65   $��Center=�6C   5�      5�      �� 5�      #��[ 6   $��56��5�      $��Stretch=�6)   $
��Left=�5�      #��[ 5�      $	��8,4q�5�      ��5�      �� *NavigateToStudentsCommand+.��5�      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $	 AccountGroup=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Students��6(   $��16(�68   $��Center=�6F   5�      5�      �� 5�      #��[ 6   $��56��5�      $��Stretch=�6)   $
��Left=�5�      #��[ 5�      $	��8,4q�5�      ��5�      �� *NavigateToRecordsCommand+.��5�      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $	 FileDocument=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Issue/Return��6(   $��16(�6<   $��Center=�6J   5�      5�      �� 5�      #��[ 6   $��56��5�      $��Stretch=�6)   $
��Left=�5�      #��[ 5�      $	��8,4q�5�      ��5�      �� *NavigateToCoursesCommand+.��5�      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $	 School=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Courses��6(   $��16(�67   $��Center=�6E   5�      5�      5�      5�      �� 5�      $��1��6     
CreamBrush#��[ 6%   ._�5�      �� ��6#   �� *CurrentViewModel+5�      5�      5�      5�      