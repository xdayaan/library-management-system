   M S B A M L   `   `   `  ����R  NLibraryManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:LibraryManagementSystem.ViewModels"LibraryManagementSystem.ViewModels  5   	   M+clr-namespace:LibraryManagementSystem.ViewsLibraryManagementSystem.Views  5   	   .    (LibraryManagementSystem.Views.MainWindow   5   	   X TPresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35] YPresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35f bSystem.Windows.Controls.Ribbon, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OWindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   S OSystem.Xaml, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   A
viewmodels0clr-namespace:LibraryManagementSystem.ViewModels  5   	   7views+clr-namespace:LibraryManagementSystem.Views  5   	     � Title$  Library Management System��5   	   $	��800��5   	   $
��1400��6    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�5
   	    � 	Resources 5       �� DataType;   5LibraryManagementSystem.ViewModels.DashboardViewModel1   +LibraryManagementSystem.Views.DashboardView7   1LibraryManagementSystem.ViewModels.BooksViewModel-   'LibraryManagementSystem.Views.BooksView:   4LibraryManagementSystem.ViewModels.StudentsViewModel0   *LibraryManagementSystem.Views.StudentsView9   3LibraryManagementSystem.ViewModels.RecordsViewModel/   )LibraryManagementSystem.Views.RecordsView9	   3LibraryManagementSystem.ViewModels.CoursesViewModel/
   )LibraryManagementSystem.Views.CoursesView��%w  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)(�� �     5      *,	 +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   " 	 6   .R�5      
  5      5      .��5      �  � RowDefinitions 5    
   �� 5!      $
��Auto �6   �� 5"      $��* �6   .+�5&   
   �� $��0��6      HeaderGradientBrush#��[  6   $��80��6P   ._�5'      � $
��24,0q�6    � ColumnDefinitions 5(      �� 5)      $
��Auto �6'   �� 5*      $��* �6'   �� 5+      $
��Auto �6'   .+�5/      �� $��0��6    �� Orientation$ 
Horizontal=�6-   $��Center=�6F   .+�50      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ LibraryShelves=�6.   $��40��51   ,   $��40��67     LightAquaBrush#��[ 52   ,   $��0,0,16,0q�53   ,   �� 55      $��Center=�6!   .+�56      �� $��Library Management System��6$   $��20(�57   $   $
��Bold"�62     
WhiteBrush#��[ 58   $   �� 59      $��Professional Edition��6$   $��12(�5:   $   #��[ 5;   $   5<      5=      �� 5@      $��2��6   $ 
Horizontal=�6-   $��Center=�6F   .+�5A      �� $��14(�5B       $��Medium"�6.   #��[ 5C       $��Center=�5D       $��0,0,16,0q�5E       ��5A       �� *CurrentUser+5E       �� 5G        MaterialDesignIconButton#��[ 6   	 � ToolTip$	 Logout��5I      $	��8,0q�5J      ��5H      �� *LogoutCommand+.��5K        $ Logout=�62   $��24��5L   0   $��24��6;   #��[ 5M   0   5N      �� 5P      #��[ 6   $	 Exit Application��5R      $	��8,0q�5S      ��5Q      �� *ExitCommand+.��5T        $ 	ExitToApp=�62   $��24��5U   0   $��24��6;   #��[ 5V   0   5W      5X      5Y      5Z      � 5]   
   $��1��6    5^      �� 5_      $	��280 �6#   �� 5`      $��* �6#   .+�5d      �� $��0��6     	NavyBrush#��[ 6%   ._�5e      �� $��0,24,0,0q�6   .+�5g      ��   MaterialDesignFlatButton#��[ 6   $��56��5i      $��Stretch=�6)   $
��Left=�5j      #��[ 5k      $	��8,4q�5l      ��5h      �� *NavigateToDashboardCommand+.��5m      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5n        $ ViewDashboard=�66   $��24��5o   4   $��24��6?   $��0,0,16,0q�5p   4   �� 5q      $��	Dashboard��6(   $��16(�69   $��Center=�6G   5r      5s      �� 5u      #��[ 6   $��56��5w      $��Stretch=�6)   $
��Left=�5x      #��[ 5y      $	��8,4q�5z      ��5v      �� *NavigateToBooksCommand+.��5{      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5|        $
 Book=�66   $��24��5}   4   $��24��6?   $��0,0,16,0q�5~   4   �� 5      $��Books��6(   $��16(�65   $��Center=�6C   5�      5�      �� 5�      #��[ 6   $��56��5�      $��Stretch=�6)   $
��Left=�5�      #��[ 5�      $	��8,4q�5�      ��5�      �� *NavigateToStudentsCommand+.��5�      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $ AccountGroup=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Students��6(   $��16(�68   $��Center=�6F   5�      5�      �� 5�      #��[ 6   $��56��5�      $��Stretch=�6)   $
��Left=�5�      #��[ 5�      $	��8,4q�5�      ��5�      �� *NavigateToRecordsCommand+.��5�      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $ FileDocument=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Issue/Return��6(   $��16(�6<   $��Center=�6J   5�      5�      �� 5�      #��[ 6   $��56��5�      $��Stretch=�6)   $
��Left=�5�      #��[ 5�      $	��8,4q�5�      ��5�      �� *NavigateToCoursesCommand+.��5�      �� $ 
Horizontal=�6%   $
��16,0q�6>   .+�5�        $ School=�66   $��24��5�   4   $��24��6?   $��0,0,16,0q�5�   4   �� 5�      $��Courses��6(   $��16(�67   $��Center=�6E   5�      5�      5�      5�      �� 5�      $��1��6     
CreamBrush#��[ 6%   ._�5�      �� ��6#   �� *CurrentViewModel+5�      5�      5�      5�      