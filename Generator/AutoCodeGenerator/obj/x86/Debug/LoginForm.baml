   M S B A M L   `   `   `  ����  AutoCodeGenerator!    AutoCodeGenerator.LoginForm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowStartupLocation$  CenterOwner=�5       � Title$&  Auto Code Generator - Login Form��5      $	��620��6.   $	��825��6;    � Icon$ icons/favicon-16x16.png��6G    � 
ResizeMode$ NoResize=�6f    � 	Resources 5         
mybtnStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5      " ��6   .�5      ��  � �� �	#FF9CA8DC6%   �� 5	       � ��$
 Bold"�6%   �� 5
       � ��$	 8,4q�6"   �� 5       � ��$ 4q�6!   5      5      .��5      � -   5        $��
MainWindow��5   	    � ColumnDefinitions 5      �� 5      $	��66* �6   �� 5      $
��343* �6   �� 5      $��0* �6   .+�5      ]� $��0,34,6.4,0.4q�6   $��2��6&   .+�5      � 
����ح�.+�5   
   �� -   5        	 ��Name$	 label4��5   2   $��32��6   $��321,55,286,0q�6   $	��Top=�6B   $��16(�6Z   $
��Bold"�6h   $��Center=�6z   $��Stretch=�6�   ���	#FF221544$��DAL Generation Tool��6�   �� 5   
   -   5        
 ��Name$
 txtUserName��5   5   $��23��6   $��321,136,286,0q�6   $	��Top=�6J   -  'System.Windows.Input.KeyboardNavigation   TabIndex$ 0��6b   �� 5   
   -   5        $	 label4_Copy1��5   2   $��32��6   $��321,78,286,0q�6   $	��Top=�6H   $��10(�6`   $��Center=�6n   $��Stretch=�6�   ���	#FF221544$��Rev. 201707��6�   �� 5   
   $��715,35,33,494q�6   $
��Wrap=�6+    �� TextAlignment$ Center=�6?   �� 6W   -   5        $G��AM:\ComputerFiles\_AppDev\UjjTool\_VersionControl\FAQ\CADSFaq.docx?�5   �   .�6�   �� $	�FAQ��6�   6�   6�   �� 5   
   -   5        $	 label1��5   L   $��28��6   $
��Left=�6   $��201,133,0,0q�67   $	��Top=�6\   $	��120��6t   $��	User Name��6�   �� 5   
   -   5        $	 label2��5   L   $��28��6   $
��Left=�6   $��201,178,0,0q�67   $	��Top=�6\   $	��120��6t   $��Password��6�   G� 5   
   -	   5         G�Name$ txtPassword��5   9   $��23��6   $��321,180,286,0q�6"   $	��Top=�6N   $ 1��6f   �� 5   
   -
   5        $	 lblError��5   2   $��28��6   $��139,0,92,211q�6   $��Bottom=�6D   
���  ��$y�Hidden=�6p   $��Center=�6�   �� 5   
   -   5        $	 label3��5   L   $��28��6   $
��Left=�6   $��201,223,0,0q�67   $	��Top=�6\   $	��120��6t   $��Environment��6�   �� 5    
   -   5         ��Name$ cbEnvironment��5    6   $��23��6   $��321,223,286,0q�6   $	��Top=�6M   $ 2��6e   .@�5!      ��  �� 
IsSelected . $��
PRODUCTION��6+   5&      �� 5'   
   -   5         ��Name$ 	btnVerify��5'   N   #��[  6   $��321,266,286,254q�65   $��Verify Login Information��6y   $ 3��6�   5(   	   5)      �� 5*      -   5         ��Name$ image1��5*       $��
0,0,35.4,0q�6   $
��Fill=�60   $��68��6?   $ ��icons/apple-icon-72x72.png��6J   $��Right=�6n   $��62��6�   $	��Top=�6�   $��1��6�   ]� 5+      $��0q�6   $��34��6   $	��Top=�6'   $
��Left=�6?   $	��150��6Z   $��2��6f   .+�5,      � .+�5-      �� -   5        $	 	loginform��5-      $��16(�6+   $��10,0,-154,0q�69   $��Auto Code Generator...��6N   5.   	   5/      50      51      