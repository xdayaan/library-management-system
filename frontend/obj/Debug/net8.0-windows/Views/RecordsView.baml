   M S B A M L   `   `   `  ����  LibraryManagementSystem/    )LibraryManagementSystem.Views.RecordsView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      � $��32q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   �� 5
      $
��Auto �6   .+�5   
   � $��0��6   $��0,0,0,24q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   .+�5      �� $��Issue/Return Records��6      HeaderTextStyle#��[  68   �� 5      $%��Manage book lending and returns��6   $��16(�5        DarkGrayBrush#��[ 5      5      �� 5      $��1��6     PrimaryButtonStyle#��[ 5      $��40��5      $	��130��6!   ��5      �� *OpenIssueDialogCommand+.��5      ��  �� Orientation$ 
Horizontal=�6   .+�5       '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ BookPlus=�6.   $��16��6>   $��16��6I   $��0,0,8,0q�6U   �� 5!      $��
Issue Book��6    5"      5#      5$      #  MaterialDesignThemes.Wpf.Card  5'   
   $��1��6     	CardStyle#��[ 6+   $��0q�6N   .��5(      &   System.Windows.Controls.DataGrid    ModernDataGridStyle#��[ 5*         AutoGenerateColumns$ False��5+         CanUserAddRows$ False��5,         CanUserDeleteRows$ False��5-         
IsReadOnly$
 True��5.         SelectionMode$ Single=�5/      	   GridLinesVisibility$	 
Horizontal=�50      
   HeadersVisibility$
 Column=�51      ��5(      �� *	Records+ �� SelectedItem 5)      �� *SelectedRecord+   Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Record ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 56   )    ) %MaterialDesignDataGridTextColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 57   )      Binding 55   )   �� *Id+57   )     59      $ 	Book Name��6)   $	 200 5;   )   # [ 5<   )    5:   )   �� *
BookName+5<   )     5>      $ Student Name��6)   $	 180 5@   )   # [ 5A   )    5?   )   �� *StudentName+5A   )     5C      $ 
Issue Date��6)   $	 100 5E   )   # [ 5F   )    5D   )   �� *	IssueDate+ �� StringFormat$ 
dd/MM/yyyy��5F   )     5H      $ Due Date��6)   $	 100 5J   )   # [ 5K   )    5I   )   �� *	DueDate+$ 
dd/MM/yyyy��5K   )     5M      $ Return Date��6)   $	 100 5O   )   # [ 5P   )    5N   )   �� *
ReturnDate+$ 
dd/MM/yyyy��5P   )     5R      $
 Fine��6)   $ 80 5T   )   # [ 5U   )    5S   )   �� *Fine+$ ₹{0}��5U   )   4  .System.Windows.Controls.DataGridTemplateColumn  5W      $ Status��6-   $	 100 6=      CellTemplate 5X      �� 5Y      .R�5Z   "   ��  �� CornerRadius$ 12��6)    �� Padding$	 8,4q�6;   ��5[   &   �� 5\   *    �� 
TargetType" ��60    �� Triggers 5]   .   �� 5^   2    �� Binding 6>   �� *
StatusText+ �� Value Returned6]   .a�5_   6   ��  �� Property � ��  SuccessBrush �� Value# [ 6S   5`   3   �� 5a   2    6>   �� *
StatusText+ Overdue6]   .a�5b   6   ��  � ��  
ErrorBrush# [ 6S   5c   3   �� 5d   2    6>   �� *
StatusText+
 Issued6]   .a�5e   6   ��  � ��  	InfoBrush# [ 6S   5f   3   5h   +   ._�5j   &   ��  	 
WhiteBrush#��[	 5k   0   $��Medium"�5l   0   $��11(�5m   0   $��Center=�5n   0   ��5j   0   �� *
StatusText+5n   0   5o   #   5p      5r        5t      $ Actions��6-   $	 100 6>    5u      �� 5v      .R�5w   "   �� #��[ 6)   $��Return��5z   )   $��32��5{   )   $��80��65   $��11(�5|   )   ��5x   )   �� *DataContext.ReturnBookCommand+ �� RelativeSource ��  �� AncestorType UserControl��5y   )   �� y�5}   )   �� *
IsReturned+ 
 InverseBoolToVisConverter �� 	Converter# [
 5~      5�      5�      5�        5�   
   $��2��6   #��[ 6+   $��0,24,0,0q�6N   $��16q�6`   .��5�      �  5�      �� 5�      $
��Auto �6'   �� 5�      $��* �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   $ 
Horizontal=�6-   $��Center=�6F   .+�5�      �� $��Page:��6    $��Center=�6-   $��0,0,8,0q�6H   �� 5�      $
��Bold"�6=   $��Center=�6O   $��0,0,8,0q�6j   ��6    �� *CurrentPage+6j   �� 5�      $��of��6    $��Center=�6*   $��0,0,8,0q�6E   �� 5�      $
��Bold"�6<   $��Center=�6N   $��0,0,16,0q�6i   ��6    �� *
TotalPages+6i   �� 5�      $��Center=�6`   #��[ 6{   ��6    �� *
TotalItems+$ ({0} total records)��6{   5�      �� 5�      $��2��6   $ 
Horizontal=�6-   .+�5�      ��   MaterialDesignIconButton#��[ 6    � ToolTip$ Previous Page��5�      ��5�      �� *PreviousPageCommand+.��5�        $ ChevronLeft=�62   $��20��6E   $��20��6P   5�      �� 5�      #��[ 6   $ 	Next Page��5�      ��5�      �� *NextPageCommand+.��5�        $ ChevronRight=�62   $��20��6F   $��20��6Q   5�      5�      5�      5�      � 5�   
   $��3��6   ���	#80000000y�5�      �� *IsIssueDialogOpen+   BooleanToVisibilityConverter# [ .+�5�        $��Center=�6"   $��Center=�5�       $	��500��5�       $��32q�5�       .��5�      �� .+�5�      �� $��
Issue Book��6    #��[  62   �� 5�        ModernComboBoxStyle#��[ 6   )	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$ Select Book*��5�        �� SelectedValuePath$  Id��5�      ! �� DisplayMemberPath$
! Name��5�      $	��0,8q�5�      ��5�      �� *Books+" �� SelectedValue" 5�      �� *SelectedBookId+5�      �� 5�      #��[ 6   $ Select Student*��5�      $  Id��5�      $
! Name��5�      $	��0,8q�5�      ��5�      �� *
Students+" 5�      �� *SelectedStudentId+5�      (
  "System.Windows.Controls.DatePicker
  5�       " MaterialDesignFilledDatePicker#��[ 6!   $ 	Due Date*��5�   !   $��48��5�   !   $	��0,8q�5�   !   # 
  SelectedDate# 5�   !   �� *	DueDate+5�   !   �� 5�      #��[ 5�       $��Medium"�5�       $
��Wrap=�5�       $��0,16,0,0q�5�       ��5�       �� *ErrorMessage+y�5�       �� *ErrorMessage+  StringToVisibilityConverter# [ �� 5�      #��[ 5�       $��Medium"�5�       $
��Wrap=�5�       $��0,16,0,0q�5�       ��5�       �� *SuccessMessage+y�5�       �� *SuccessMessage+# [ �� 5�      $ 
Horizontal=�6!   $��Right=�5�   !   $��0,24,0,0q�5�   !   .+�5�      �� $��Cancel��6!     SecondaryButtonStyle#��[ 5�   !   $	��100��5�   !   $��0,0,16,0q�5�   !   ��5�   !   �� *CancelIssueDialogCommand+5�   !   �� 5�      $��
Issue Book��6!   #��[ 5�   !   $	��120��5�   !   ��5�   !   �� *IssueBookCommand+{�5�   !   �� *	IsLoading+  InverseBooleanConverter# [ 5�   !   5�      5�      5�      5�      � 5�   
   $��3��6   ���	#80000000y�5�      �� *	IsLoading+# [ .+�5�      �� $��Center=�6   $��Center=�66   .+�5�      �  % !MaterialDesignCircularProgressBar#��[ 6   $��48��5�      $��48��6(     	TealBrush#��[ 5�      �� 5�      $��
Loading...��6   $��16(�5�      $��Medium"�6*   #��[	 5�      $��Center=�5�      $��0,16,0,0q�5�      5�      5�      5�      5�      