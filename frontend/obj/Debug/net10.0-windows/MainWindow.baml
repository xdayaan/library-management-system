   M S B A M L   `   `   `  ����<  8., Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:__  5   	       _.MainWindow   5   	   X TPresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35] YPresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35f bSystem.Windows.Controls.Ribbon, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OWindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   S OSystem.Xaml, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   !localclr-namespace:_     5   	     � Title$  Library Management System��5   	    � WindowState$ 	Maximized=�5	   	   ���#F0F0F0 � 	Resources 5       �� Source�� 5   
   $ Styles.xaml?�6   %    .��5      � .+�5   
   � -   5         �Name$ 
LoginPanel��5      $y�Visible=�6#   .+�5      �� $	��400��6   $	��400��6!   
������� �� CornerRadius$ 10��5         DropShadowEffect J� Effect# [  6'   ._�5      �� $��30q�6   .+�5      �� $��Library Management System��6      HeaderTextStyle#��[ 5       $��0,0,0,30q�5       �� 5      $��Username��6      LabelTextStyle#��[ 60   �� 5      -   5         ��Name$ UsernameTextBox��5      $��0,5,0,15q�67     InputTextBoxStyle#��[ 5      �� 5      $��Password��6    #��[ 60   G� 5      -   5         G�Name$ PasswordBox��5   "   $��0,5,0,30q�67     PasswordBoxStyle#��[ 5    "   �� 5"      -   5        	 ��Name$	 LoginButton��5"      $��Login��62     PrimaryButtonStyle#��[ 5#      $��0,0,0,15q�5%      $��10, 5q�6/   �� 5'      $��Forgot Password?��6      LinkTextStyle#��[ 5(       $��Center=�5)       5*      5+      5,      � 5/   
   -   5        $ DashboardPanel��5/      $y�	Collapsed=�6'   .+�51      � ���#FFFFFF$��60��6(   $	��Top=�64   # [  52      
 � ColumnDefinitions
 53      �� 54      $
��Auto �6'   �� 55      $��* �6'   �� 56      $
��Auto �6'   .+�59      �� $��0��6    �� Orientation$ 
Horizontal=�6-   $
��15,0q�6F   .+�5:      �� -   5        $��Students��5:        NavButtonStyle#��[ 60   $	��5,0q�6X   6e   �� 5;      -   5        $��Books��5;      #��[ 6-   $	��5,0q�6U   6b   �� 5<      -   5        $��Courses��5<      #��[ 6/   $	��5,0q�6W   6d   �� 5=      -	   5        $��Records��5=      #��[ 6/   $	��5,0q�6W   6d   5>      �� 5@      $��2��6   $ 
Horizontal=�6-   $��Center=�6F   $
��15,0q�6a   .+�5A      �� -
   5         ��Name$ LibrarianName��5A       $��Librarian Name��67     UserNameTextStyle#��[ 5B       $��Center=�5C       $��0,0,15,0q�6;   �� 5E      -   5        $��Logout��5E       	 LogoutButtonStyle#��[	 6.   5F      5G      5H      � 5K      $��0,60,0,0q�6   .+�5M      � -   5        $ WelcomePanel��5M      $y�Visible=�6-   .+�5N      �� $*��$Welcome to Library Management System��6     
 WelcomeTextStyle#��[
 5O       $��Center=�5P       $��Center=�5Q       5R      � 5U      -   5        $ StudentsPanel��5U      $y�	Collapsed=�6.    � RowDefinitions 5V      �� 5W      $
��Auto �6(   �� 5X      $��* �6(   �� 5Y      $
��Auto �6(   .+�5\      �� $��0��6    $��Student Management��6-   $��24(�5]       $
��Bold"�6.   $��20,10q�6@   &   System.Windows.Controls.DataGrid  5_      -   5          Name$ StudentsGrid��5_      $��1��65   $��20q�6B      AutoGenerateColumns$ False��5`         
IsReadOnly$
 True��5a         SelectionMode$ Single=�5b         AlternatingRowBackground
 �����   Columns 5d      0  *System.Windows.Controls.DataGridTextColumn  5e      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Id+6T     5f      $ Roll Number��61   $	 100 6e    6F   �� *
RollNumber+6e     5g      $
 Name��61   $	 150 6X    6?   �� *Name+6X     5h      $ Email��61   $	 200 6Z    6@   �� *Email+6Z     5i      $ Phone��61   $	 100 6Z    6@   �� *Phone+6Z     5j      $ Gender��61   $ 70 6\    6A   �� *Gender+6\     5k      $
 Year��61   $ 70 6X    6?   �� *Year+6X     5l      $ Course��61   $	 100 6a    6A   �� *Course.Name+6a     5m      $ Father's Name��61   $	 150 6g    6H   �� *
FatherName+6g   5o      �� 5q      $��2��6!   $ 
Horizontal=�6.   $��Center=�6G   $��	0,10,0,20q�6d   .+�5r      �� -   5        $��Add New Student��5r   !   $��5q�6;   $
��10,5q�6F   6U   �� 5s      -   5        $��View All Students��5s   !   $��5q�6=   $
��10,5q�6H   6W   5t      5u      � 5x      -   5        $ 
BooksPanel��5x      $y�	Collapsed=�6+    5y      �� 5z      $
��Auto �6(   �� 5{      $��* �6(   .+�5~      �� $��0��6    $��Book Management��6-   $��24(�5       $
��Bold"�6.   $��20,10q�6@   �� 5�      $��1��6    $)��#Book Management Content Coming Soon��6-   $��Center=�5�       $��Center=�6=   $��20(�5�       ���#8888886.   5�      � 5�      -   5        $ CoursesPanel��5�      $y�	Collapsed=�6-    5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $��0��6    $��Course Management��6-   $��24(�5�       $
��Bold"�6.   $��20,10q�6@   �� 5�      $��1��6    $+��%Course Management Content Coming Soon��6-   $��Center=�5�       $��Center=�6=   $��20(�5�       ���#8888886.   5�      � 5�      -   5        $ RecordsPanel��5�      $y�	Collapsed=�6-    5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $��0��6    $��Record Management��6-   $��24(�5�       $
��Bold"�6.   $��20,10q�6@   �� 5�      $��1��6    $+��%Record Management Content Coming Soon��6-   $��Center=�5�       $��Center=�6=   $��20(�5�       ���#8888886.   5�      �� 5�      -   5        $ 	AuthToken��5�      $��
Auth Token��6/   #��[ 5�      $��Bottom=�5�      $��Right=�67   $��	0,0,15,15q�5�       J� Opacity$	 0.5Q�6/   5�      5�      5�      5�      