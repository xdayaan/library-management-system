   M S B A M L   `   `   `  ����  LibraryManagementSystem0    *LibraryManagementSystem.Views.StudentsView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      � $��32q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0,0,0,24q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   .+�5      �� $��Students Management��6      HeaderTextStyle#��[  67   �� 5      $2��,Manage student registrations and information��6   $��16(�5        DarkGrayBrush#��[ 5      5      �� 5      $��1��6     SecondaryButtonStyle#��[ 5      $��40��5       $	��100��6!   $��0,0,16,0q�5!      ��5      �� *SearchStudentsCommand+.��5"      ��  �� Orientation$ 
Horizontal=�6   .+�5#      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6.   $��16��6=   $��16��6H   $��0,0,8,0q�6T   �� 5$      $��Search��6    5%      5&      �� 5(      $��2��6     PrimaryButtonStyle#��[ 5)      $��40��5+      $	��130��6!   ��5*      �� *OpenAddDialogCommand+.��5,      �� $ 
Horizontal=�6   .+�5-        $
 Plus=�6.   $��16��6:   $��16��6E   $��0,0,8,0q�6Q   �� 5.      $��Add Student��6    5/      50      51      #  MaterialDesignThemes.Wpf.Card  54   
   $��1��6     	CardStyle#��[ 6+   $��0,0,0,24q�6N   $��24,16q�6`   .��55      �  56      �� 57      $��* �6'   �� 58      $
��Auto �6'   .+�5;      �� $��0��6     ModernTextBoxStyle#��[ 5<      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$7 1Search students by name, email, or roll number...��5=      $��0,0,16,0q�5?      ��5>      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�5?      �� 5A      $��1��6   $ 
Horizontal=�6-   .+�5B      �� $��Center=�5C       #��[ 5D       $��Medium"�5E       $��0,0,16,0q�5F       ��5B       �� *
TotalItems+ �� StringFormat$ Total: {0} students��5F       5G      5H      5I        5L   
   $��2��6   #��[ 6+   $��0q�6N   .��5M      &   System.Windows.Controls.DataGrid    ModernDataGridStyle#��[ 5O         AutoGenerateColumns$ False��5P         CanUserAddRows$ False��5Q      	   CanUserDeleteRows$	 False��5R      
   
IsReadOnly$

 True��5S         SelectionMode$ Single=�5T         GridLinesVisibility$ 
Horizontal=�5U         HeadersVisibility$ Column=�5V      ��5M      �� *
Students+ �� SelectedItem 5N      �� *SelectedStudent+   Columns 5X      0  *System.Windows.Controls.DataGridTextColumn  5Y      ,  &System.Windows.Controls.DataGridColumn   Header$ Roll #��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 5[   )    ) %MaterialDesignDataGridTextColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5\   )      Binding 5Z   )   �� *
RollNumber+5\   )     5^      $ Student Name��6)   $	 200 5`   )   # [ 5a   )    5_   )   �� *Name+5a   )     5c      $ Email��6)   $	 200 5e   )   # [ 5f   )    5d   )   �� *Email+5f   )     5h      $ Phone��6)   $	 120 5j   )   # [ 5k   )    5i   )   �� *Phone+5k   )     5m      $ Course��6)   $	 120 5o   )   # [ 5p   )    5n   )   �� *
CourseName+5p   )     5r      $
 Year��6)   $ 80 5t   )   # [ 5u   )    5s   )   �� *Year+5u   )     5w      $ Gender��6)   $ 80 5y   )   # [ 5z   )    5x   )   �� *Gender+5z   )     5|      $ Father's Name��6)   $	 150 5~   )   # [ 5   )    5}   )   �� *
FatherName+5   )   4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      $ Actions��6-   $	 150 6>    	  CellTemplate 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   $��Center=�6F   .+�5�   &   ��   MaterialDesignIconButton#��[ 6-    � ToolTip$ Edit Student��5�   -   $	��4,0q�5�   -   ��5�   -   �� *#!DataContext.OpenEditDialogCommand+ �� RelativeSource ��  �� AncestorType UserControl��5�   -   �� .��5�   *     $
 Edit=�6B   $��16��6N   $��16��6Y    	 	TealBrush#��[	 6e   5�   '   �� 5�   &   #��[ 6-   $ Delete Student��5�   -   $	��4,0q�5�   -   ��5�   -   �� *" DataContext.DeleteStudentCommand+ ��  UserControl��5�   -   �� .��5�   *     $ Delete=�6B   $��16��6P   $��16��6[    
 
ErrorBrush#��[
 6g   5�   '   5�   #   5�      5�      5�      5�        5�   
   $��3��6   #��[ 6+   $��0,24,0,0q�6N   $��16q�6`   .��5�      �  5�      �� 5�      $
��Auto �6'   �� 5�      $��* �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   $ 
Horizontal=�6-   $��Center=�6F   .+�5�      �� $��Page:��6    $��Center=�6-   $��0,0,8,0q�6H   �� 5�      $
��Bold"�6=   $��Center=�6O   $��0,0,8,0q�6j   ��6    �� *CurrentPage+6j   �� 5�      $��of��6    $��Center=�6*   $��0,0,8,0q�6E   �� 5�      $
��Bold"�6<   $��Center=�6N   $��0,0,16,0q�6i   ��6    �� *
TotalPages+6i   �� 5�      $��Center=�6^   #��[ 6y   ��6    �� *
TotalItems+$ ({0} total items)��6y   5�      �� 5�      $��2��6   $ 
Horizontal=�6-   .+�5�      �� #��[ 6   $ Previous Page��5�      ��5�      �� *PreviousPageCommand+.��5�        $ ChevronLeft=�62   $��20��6E   $��20��6P   5�      �� 5�      #��[ 6   $ 	Next Page��5�      ��5�      �� *NextPageCommand+.��5�        $ ChevronRight=�62   $��20��6F   $��20��6Q   5�      5�      5�      5�      � 5�   
   $��4��6   ���	#80000000y�5�      �� *IsAddEditDialogOpen+   BooleanToVisibilityConverter �� 	Converter# [ .+�5�        $��Center=�6"   $��Center=�5�       $	��700��5�       $	��800��5�       $��32q�5�       .��5�      �� $
��Auto=�6   .��5�      �� .+�5�      �� #��[  5�   $   ��5�   $   �� *
IsEditMode+  BooleanToTextConverter# [  �� ConverterParameter$" Edit Student|Add New Student��5�   $   � 5�       5�      �� 5�   "   $��* �63   �� 5�   "   $��* �63     5�      �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   .+�5�      �� $��0��6&   $��0��63   #��[ 5�   &   $ Roll Number��5�   &   $��0,0,8,0q�5�   &   ��5�   &   �� *NewStudent.RollNumber+5�   &   �� 5�      $��0��6&   $��1��63   #��[ 5�   &   $ Student Name*��5�   &   $��8,0,0,0q�5�   &   ��5�   &   �� *NewStudent.Name+5�   &   �� 5�      $��1��6&   $��0��63   #��[ 5�   &   $ Email*��5�   &   $��0,0,8,0q�5�   &   ��5�   &   �� *NewStudent.Email+5�   &   �� 5�      $��1��6&   $��1��63   #��[ 5�   &   $ Phone��5�   &   $��8,0,0,0q�5�   &   ��5�   &   �� *NewStudent.Phone+5�   &   �� 5�      $��2��6'   $��0��64     ModernComboBoxStyle#��[ 5�   '   $ Course*��5�   '    �� SelectedValuePath$ Id��5�   '    �� DisplayMemberPath$
 Name��5�   '   $��0,0,8,0q�5�   '   ��5�   '   �� *	Courses+ �� SelectedValue 5�   '   �� *NewStudent.CourseId+5�   '   �� 5�      $��2��6&   $��1��63   #��[ 5�   &   $
 Year��5�   &   $��8,0,0,0q�5�   &   ��5�   &   �� *NewStudent.Year+5�   &   �� 5     $��3��6'   $��0��64   #��[ 5  '   $ Gender��5  '    �� 
IsEditable$
 True��5  '   $��0,0,8,0q�5  '    �� Text 5  '   �� *NewStudent.Gender+.@�5  "   �� $
��Male��6/   �� 5  "   $��Female��6/   �� 5	  "   $��Other��6/   5
     �� 5     $��3��6&   $��1��63   #��[ 5  &   $ Father's Name��5  &   $��8,0,0,0q�5  &   ��5  &   �� *NewStudent.FatherName+5  &   �� 5     $��4��6&   $��2��63   #��[ 5  &   $ Address��5  &    �� AcceptsReturn$
 True��5  &     �� TextWrapping$
  Wrap=�5  &   $��80��5  &   $	��Top=�5  &   ��5  &   �� *NewStudent.Address+5  &   �� 5     $��5��6&   $��0��63   #��[ 5  &   $ 	Photo URL��5  &   $��0,0,8,0q�5  &   ��5  &   �� *NewStudent.Photo+5  &   G� 5!     $��5��6*   $��1��67    # MaterialDesignFilledPasswordBox#��[ 5"  )   $ Password��5#  )     LightGrayBrush!   
Background#! [ 5$  )   .
  (MaterialDesignThemes.Wpf.TextFieldAssist" 
  UnderlineBrush#" [	 5%  )   $��48��5&  )   $��8,0,0,0q�5'  )   5(     �� 5+     #��[
 5,  $   $��Medium"�5-  $   $
��Wrap=�5.  $   $��0,16,0,0q�5/  $   ��5+  $   �� *ErrorMessage+y�50  $   �� *ErrorMessage+  StringToVisibilityConverter# [ �� 52       SuccessBrush#��[ 53  $   $��Medium"�54  $   $
��Wrap=�55  $   $��0,16,0,0q�56  $   ��52  $   �� *SuccessMessage+y�57  $   �� *SuccessMessage+# [ �� 5:     $ 
Horizontal=�6%   $��Right=�5;  %   $��0,24,0,0q�5<  %   .+�5=     �� $��Cancel��6%   #��[ 5>  %   $	��100��5@  %   $��0,0,16,0q�5A  %   ��5?  %   �� *CancelDialogCommand+5A  %   �� 5C     #��[ 5D  %   $	��100��5G  %   ��5C  %   �� *
IsEditMode+# [ $ Update|Save����5E  %   �� *SaveStudentCommand+{�5F  %   �� *	IsLoading+  InverseBooleanConverter# [ 5G  %   5H     5I     5J     5K     5L     � 5O  
   $��4��6   ���	#80000000y�5Q     �� *	IsLoading+# [ .+�5R     �� $��Center=�6   $��Center=�66   .+�5S     �  % !MaterialDesignCircularProgressBar#��[ 6   $��48��5T     $��48��6(   #��[	 5U     �� 5V     $��
Loading...��6   $��16(�5W     $��Medium"�6*     
WhiteBrush#��[ 5X     $��Center=�5Y     $��0,16,0,0q�5Z     5[     5\     5]     5^     