   M S B A M L   `   `   `  ����\  XGMap.NET.WindowsForms, Version=1.7.0.0, Culture=neutral, PublicKeyToken=b85b9027b614afef\Bclr-namespace:GMap.NET.WindowsForms;assembly=GMap.NET.WindowsFormsGMap.NET.WindowsForms  5   	   T PGMap.NET.Core, Version=1.7.0.0, Culture=neutral, PublicKeyToken=b85b9027b614afef:-clr-namespace:GMap.NET;assembly=GMap.NET.CoreGMap.NET 5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   B >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1 5	   	      WpfApp1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   RgmfBclr-namespace:GMap.NET.WindowsForms;assembly=GMap.NET.WindowsForms     5   	   <gm-clr-namespace:GMap.NET;assembly=GMap.NET.Core     5   	   Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   	   'localclr-namespace:WpfApp1     5	   	     � Title$  
MainWindow��5   	   $
��1000��6   $
��1000��6*   
���   �.��5      � .+�5   
   ]� .+�5      �� $	��Top=�6   
���   �$	��269��6A   $	��998��6N   .+�5      �� -   5         ��Name$ 
LoadButton��5      $��
Load Model��6-   $	��150��6[   $��5q�6g   �� 5      -   5         ��Name$ SliderScale��5       � Maximum$ 50Q�6.    � Minimum$ 0Q�6;    � Value$ 1Q�6G   $	��Top=�6Q   	� 5      -   5        $��Polygon��5      
�������$��39��6D   $	��136��6P   $��Polygon��6{   	� 5      -   5        $��Ellipse��5      
�������$��33��6D   $	��136��6P   $��Ellipse��6z   	� 5      -   5        $
��Text��5      
�������$��27��6A   $	��136��6M   $
��Text��6x   �� 5      -   5        $��
UndoButton��5   (   $
��Undo��6   $��40��6:   $	��160��6F   $y�Hidden=�6R   6f   �� 5      -   5        $��
RedoButton��5   (   $
��Redo��6   $��40��6:   $	��160��6F   $y�Hidden=�6R   6f   �� 5      -   5        $��ClearButton��5   )   $��Clear��6   $��40��6<   $	��160��6H   $y�Hidden=�6T   6h   5      �� 5      $
��Auto=�6   $
��Auto=�6@   $	��621��6c   .��5      �� -	   5         ��Name$
 Mapa��5      
�������$	��500��6:   $	��500��6F    � LayoutTransform 5      �� 5        �� ScaleX 6)   �� 	 �� ElementName$	 SliderScale��
 �� Path$
 Value� �� ScaleY 5!   !   �� $	 SliderScale��$
 Value�5#      5$      5%      5&      5'      