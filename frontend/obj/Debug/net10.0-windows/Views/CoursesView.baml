   M S B A M L   `   `   `  ����  LibraryManagementSystem/    )LibraryManagementSystem.Views.CoursesView   5      X TPresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35] YPresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35f bSystem.Windows.Controls.Ribbon, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OWindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      S OSystem.Xaml, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      � $��32q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   .+�5   
   � $��0��6   $��0,0,0,24q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   .+�5      �� $��Courses Management��6      HeaderTextStyle#��[  66   �� 5      $*��$Manage academic courses and programs��6   $��16(�5        DarkGrayBrush#��[ 5      5      �� 5      $��1��6     PrimaryButtonStyle#��[ 5      $��40��5      $	��130��6!   ��5      �� *OpenAddDialogCommand+.��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6.   $��16��6:   $��16��6E   $��0,0,8,0q�6Q   �� 5       $��
Add Course��6    5!      5"      5#      #  MaterialDesignThemes.Wpf.Card  5&   
   $��1��6     	CardStyle#��[ 6+   $��0q�6N   .��5'      &   System.Windows.Controls.DataGrid    ModernDataGridStyle#��[ 5)         AutoGenerateColumns$ False��5*         CanUserAddRows$ False��5+         CanUserDeleteRows$ False��5,         
IsReadOnly$
 True��5-         SelectionMode$ Single=�5.      	   GridLinesVisibility$	 
Horizontal=�5/      
   HeadersVisibility$
 Column=�50      ��5'      �� *	Courses+ �� SelectedItem 5(      �� *SelectedCourse+   Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Course ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 55   )    ) %MaterialDesignDataGridTextColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 56   )      Binding 54   )   �� *Id+56   )     58      $ Course Name��6)   $	 300 5:   )   # [ 5;   )    59   )   �� *Name+5;   )     5=      $ 
Created On��6)   $	 150 5?   )   # [ 5@   )    5>   )   �� *	CreatedOn+ �� StringFormat$ dd/MM/yyyy HH:mm��5@   )     5B      $ 
Updated On��6)   $	 150 5D   )   # [ 5E   )    5C   )   �� *	UpdatedOn+$ dd/MM/yyyy HH:mm��5E   )   4  .System.Windows.Controls.DataGridTemplateColumn  5G      $ Actions��6-   $	 150 6>      CellTemplate 5H      �� 5I      .R�5J   "   �� $ 
Horizontal=�6-   $��Center=�6F   .+�5K   &   ��   MaterialDesignIconButton#��[ 6-    � ToolTip$ Edit Course��5N   -   $	��4,0q�5O   -   ��5L   -   �� *#!DataContext.OpenEditDialogCommand+ �� RelativeSource ��  �� AncestorType UserControl��5M   -   �� .��5P   *     $
 Edit=�6B   $��16��6N   $��16��6Y     	TealBrush#��[ 6e   5Q   '   5R   #   5S      5U      5W      5X      � 5[   
   $��2��6   ���	#80000000y�5]      �� *IsAddEditDialogOpen+   BooleanToVisibilityConverter �� 	Converter# [ .+�5^        $��Center=�6"   $��Center=�5_       $	��400��5`       $��32q�5a       .��5b      �� .+�5c      �� #��[  5d       ��5c       �� *
IsEditMode+ 	 BooleanToTextConverter# [	  �� ConverterParameter$  Edit Course|Add New Course��5d       �� 5f       
 ModernTextBoxStyle#��[
 6   )	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$ Course Name*��5g      $
��0,16q�5i      ��5h      �� *NewCourse.Name+5i      �� 5l        
ErrorBrush#��[ 5m       $��Medium"�5n       $
��Wrap=�5o       $��0,16,0,0q�5p       ��5l       �� *ErrorMessage+y�5q       �� *ErrorMessage+  StringToVisibilityConverter# [ �� 5s        SuccessBrush#��[ 5t       $��Medium"�5u       $
��Wrap=�5v       $��0,16,0,0q�5w       ��5s       �� *SuccessMessage+y�5x       �� *SuccessMessage+# [ �� 5{      $ 
Horizontal=�6!   $��Right=�5|   !   $��0,24,0,0q�5}   !   .+�5~      �� $��Cancel��6!     SecondaryButtonStyle#��[ 5   !   $	��100��5�   !   $��0,0,16,0q�5�   !   ��5�   !   �� *CancelDialogCommand+5�   !   �� 5�      #��[ 5�   !   $	��100��5�   !   ��5�   !   �� *
IsEditMode+# [	 $ Update|Save����5�   !   �� *SaveCourseCommand+{�5�   !   �� *	IsLoading+  InverseBooleanConverter# [ 5�   !   5�      5�      5�      5�      � 5�   
   $��2��6   ���	#80000000y�5�      �� *	IsLoading+# [ .+�5�      �� $��Center=�6   $��Center=�66   .+�5�      �  % !MaterialDesignCircularProgressBar#��[ 6   $��48��5�      $��48��6(   #��[ 5�      �� 5�      $��
Loading...��6   $��16(�5�      $��Medium"�6*     
WhiteBrush#��[ 5�      $��Center=�5�      $��0,16,0,0q�5�      5�      5�      5�      5�      