   M S B A M L   `   `   `  ����  .    _.StudentManagement   5   	   X TPresentationCore, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35] YPresentationFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35f bSystem.Windows.Controls.Ribbon, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OWindowsBase, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   S OSystem.Xaml, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Student Management��5   	   $	��600��6$   $
��1000��61   .��5      �  � RowDefinitions 5      �� 5      $��* �6   �� 5      $
��Auto �6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ StudentsGrid��5      $��0��6%   $��10q�62      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         SelectionMode$ Single=�5         AlternatingRowBackground
 �����   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   
   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 50 6@   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6)   �� *Id+6@     5   
   $ Roll Number��6   $		 100 6Q   
 62   �� *
RollNumber+6Q     5   
   $
 Name��6   $		 150 6D   
 6+   �� *Name+6D     5   
   $ Email��6   $		 200 6F   
 6,   �� *Email+6F     5   
   $ Phone��6   $		 100 6F   
 6,   �� *Phone+6F     5   
   $ Gender��6   $	 70 6H   
 6-   �� *Gender+6H     5   
   $
 Year��6   $	 70 6D   
 6+   �� *Year+6D     5   
   $ Course��6   $		 100 6M   
 6-   �� *Course.Name+6M     5   
   $ Father's Name��6   $		 150 6S   
 64   �� *
FatherName+6S   5      �� 5      $��1��6    �� Orientation$ 
Horizontal=�6   $��Center=�67   $��	0,10,0,20q�6T   .+�5      �� -   5         ��Name$ 	AddButton��5      $��Add New Student��6"   $��5q�6<   $
��10,5q�6G   6V   �� 5      -   5        $ UpdateButton��5      $��Update Selected��6%   $��5q�6?   $
��10,5q�6J   6Y   �� 5       -   5        $ DeleteButton��5       $��Delete Selected��6%   $��5q�6?   $
��10,5q�6J   6Y   5!      5"      5#      