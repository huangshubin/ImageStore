   M S B A M L   `   `   `  ����F  BImageClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageClientImageClient  5   	   !    ImageClient.Views.LoginView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ImageClient     5   	   $��18(�5      $	��500��5   
   $	��600��6   .��5	      � $��20 30 10 20q�6     � RowDefinitions  5
   
   �� 5      $��70 �6   �� 5      $
��auto �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��0��6    �� TextAlignment$ Center=�6"   �� 5      $��32(�6   .�6!   Web Image Store Client69   5      �� 5   
   $
��Wrap=�6   $��0 0 0 10q�6G   
���  ��$��1��6�   ��6(   �� *FailureReason+y�6k   �� *	Failure+   	BoolToVis �� 	Converter# [  6�   �� 5   
   $
��0 10q�6   $��2��6#    �� Orientation$ 
Horizontal=�60   .+�5      �� $	��140��6   $��0 0 10 0q�6$   $ Right=�67   
User Name:6Y   �� 5      $	��320��6   ��6"   �� *
UserName+5      �� 5   
   $
��0 10q�6   $ 
Horizontal=�6#   $��3��6<   .+�5      �� $	��140��6   $��0 0 10 0q�6$   $ Right=�66   	Password:6W   G� 5      -   5        $��
pbPassword��5   &   $	��320��6   6&   5      �� 5   
   $
��0 10q�6   $��4��6   $	��180��6-   {�69   �� *
CanExecute+ �� UpdateSourceTrigger$ PropertyChanged=���6~   ��  �� ElementName$ 
pbPassword����6�   �� *LoginCommand+.��6�   Login6�   5      5       