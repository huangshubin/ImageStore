   M S B A M L   `   `   `  ����F  BImageClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageClientImageClient  5   	   %    ImageClient.Views.ImageSendView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ImageClient     5   	   $��18(�5      $	��500��5   
   $	��600��6   .��5	      � $��20 30 10 20q�6     � RowDefinitions  5
   
   �� 5      $��30 �6   �� 5      $��70 �6   �� 5      $
��auto �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $
��auto �6   .+�5   
   ��    Link#��[  6   $
��10 0q�6S   $��Right=�6a   $��0��6}   ��60   �� *LogoutCommand+.��6�   Logout6�   �� 5   
   $��1��6    �� TextAlignment$ Center=�6"   �� 5      $��32(�6   .�6!   Web Image Store Client69   5      �� 5   
   $
��Wrap=�6   $��0 0 0 10q�6A   
���  ��$��2��6�   ��6(   �� *	Message+y�6e   �� *
DisplayMsg+  	BoolToVis �� 	Converter# [ 6�   �� 5   
   $
��0 10q�6   $��3��6#    �� Orientation$ 
Horizontal=�60   .+�5      �� $	��100��6   $��0 0 10 0q�6$   $ Right=�67   Image Path:6Z   �� 5      $	��280��6    �� 
IsReadOnly . {�.  $
��10 0q�6F   ��6T   �� *	ImagePath+6q   �� 5      $	��120��6   ��6!   �� *BrowseFileCommand+.��6G   Browse6O   5      �� 5   
   $
��0 10q�6   $ 
Horizontal=�6#   $��4��6<   $��Center=�6I   .+�5      �� $��	120 0 0 0q�6    d� 	IsChecked 6+   �� *	IsStore+.��5      �� $��	Is Store?��6   $��0,0,0,0q�6.   5       5!      �� 5"   
   $
��0 20q�6   $��5��6   $	��180��6,   {�68   �� *
CanExecute+��6Y   �� *SendImageCommand+.��6~   Send6�   5#      5$      