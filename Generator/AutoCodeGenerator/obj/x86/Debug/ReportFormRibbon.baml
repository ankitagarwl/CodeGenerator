   M S B A M L   `   `   `  ����`  \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35tNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary!Microsoft.Windows.Controls.Ribbon  5   	    AutoCodeGenerator(   "AutoCodeGenerator.ReportFormRibbon   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation        5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5   	   [ribbonNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary  5   	   6my-http://schemas.microsoft.com/wpf/2008/toolkit  5   j   -   5          � WindowStartupLocation$  CenterOwner=�5       � Title$0 *ezRide Statements Generator - Actions Form��5      $	��740��68   $	��835��6E    � Icon$ favicon.ico��6Q    � 
ResizeMode$ 	CanResize=�5   $    J� 	AllowDrop .   � AllowsTransparency .   � 	Resources 5	       �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName � RowDefinitions K� ZIndex K� IsItemsHost � Color � Offset   DataGrid_Style0_Header �� Color  DataGrid_Style0_Alt0  DataGrid_Style0_Alt1  ColumnHeaderStyle@  :Microsoft.Windows.Controls.Primitives.DataGridColumnHeader  RowStyle,  &Microsoft.Windows.Controls.DataGridRow �� Triggers��%�  '��       '�� I    &	  q    &	 �    &	 �    &	 �    7   &	 D    7  7  �� 5
   
   " ��6   .�5      ��  � ��	 5      �� 5      "
 ��6&   .R�5      � .+�5      �� $��Border��6%   
����ح�
���   �$��1,1,1,1q�6^   $ 0,0,0,0��6x   ._�5   "   �� $ ContentSite��63   $��Center=�6H   $��Center=�6c   $��Header��6�   $��5,5,5,5q�6�   5      5       5      P� 5       �@��
IsSelected . . �5   "   �� $ Border��6)    � ��
	 ��ح�6S   5      P� 5       �@��
IsSelected .  . �5   "   �� $ Border��6)    � ��
	 �����6S   5      5      5      5      �� 5   
   " ��6   .�5       ��  � ��	 5!      �� 5"      "
 ��6&   .R�5#      �  5$      �� 5%   "   $
��Auto �60   �� 5&   "   $��* �60   .+�5(      �� $��0��6'   $ 1��64   $��0,0,4,-1q�6E    . 
������ 6j   �� 5)      $��1��6%   
���   �$��1q�6F   $ 
0, 0, 0, 0��6Z   ��5*   "   �� 5+   &   .��5,   *   � $ 	LightBlue��67   $ 0Q�6I   � 5-   *   $ White��67   $ 1Q�6E   5.   '   ._�50   "   �� $��SelectedContent��63   51      52      54      56      57      �� 58   
   $ 	#FF4F81BD��69   �� 59   
   $ 	#FFD0D8E8��67   �� 5:   
   $ 	#FFE9EDF4��67   �� 5;   
   "  6*   .�5<      ��  � ��8	   6+   �� 5=       � ��
	 �����6+   5>      �� 5?   
   "  6!    5@      P� 5A       �@ AlternationIndex$ 1��66   . �5B      ��  � ��8	   63   5C      P� 5D       �@ AlternationIndex$ 0��66   . �5E      ��  � ��8	  63   5F      5H      5I      .��5J      �  � ColumnDefinitions 5K   
   �� 5L      $	��46* �6   �� 5M      $
��365* �6   �� 5N      $
��418* �6   .+�5P   
   ]� $��	0,0,4.8,0q�6   $��34��6'   $	��Top=�63   $��3��6K   .+�5Q      �� -   5        $��image1��5Q      $
��Fill=�6"   $��34��61   $ ��icons/apple-icon-60x60.png��6<   $��Right=�6`   $��34��6|   $	��Top=�6�   $��0,2,0,-2q�6�   �� 5R      -   5         ��Name$ label1��5R       $��28��6   $��16(�60   $
��Left=�6>   $	��Top=�6Y   $	��762��6q   $��0,2,0,0q�6}   $��DAL Generation Tool ��6�   5S      ]� 5T   
   $��0,34,-22.2,0q�6   $	��135��6*   $	��Top=�67   $��3��6O   .+�5U      .   (Microsoft.Windows.Controls.Ribbon.Ribbon  -   5        $��mribbon��5U   ?   $��1��6      IsDropDownOpen . .@�5V      1   +Microsoft.Windows.Controls.Ribbon.RibbonTab  -   5          Name$ memberservice��5V   ;   $��Rider Services��6#   .@�5W      3   -Microsoft.Windows.Controls.Ribbon.RibbonGroup  .@�5Z      4   .Microsoft.Windows.Controls.Ribbon.RibbonButton  -   5          Name$ ValidateUSPSAddress��5Z   .      Label$ Generate API��6K   $��Center=�6`   $��65��6}   $	��125��6�   6�   5\      5]      5S     5T     �� 5U  
   $��0,165,4.8,0.4q�6   $��3��6,   .+�5V     � .+�5W     �� -   5        $��ccReportFormRibbon��5W  !   $��2��6;   -  'System.Windows.Input.KeyboardNavigation   	IsTabStop .  6p   5X     5Y     5[     5\     