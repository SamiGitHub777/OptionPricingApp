   M S B A M L   `   `   `  ����Q  MOptionPricingWPFClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:OptionPricingWPFClient.ViewOptionPricingWPFClient.View  5      S.clr-namespace:OptionPricingWPFClient.ViewModel OptionPricingWPFClient.ViewModel  5      0    *OptionPricingWPFClient.View.PricesListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:OptionPricingWPFClient.View     5      D	viewModel.clr-namespace:OptionPricingWPFClient.ViewModel     5      ���#0F0F2D  � 	Resources  5         HeaderStyle �� 
TargetType �� Property �� Value��%|   &	        �� 5   
   " ��6$   .�5      ��  � �� �#FE65846+   �� 5       � ��$ 24(�6)   5      5      .��5      �� $
��Auto=�6   
������ .��5   
   �  � RowDefinitions 5      �� 5      $��50 �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� #��[  6   $��0��5    !   $��
All prices��5!   !   $��10 0 0 0q�5"   !   �� 5$      -   5        $��	listView1��5$   q   $��	10 10 0 0q�6   $��4��6V   $
��auto��6c   $
��auto��6�   ���#FE6584���#0F0F2D �� IsTextSearchEnabled . ��6�   �� *ItemsCollection+ �� View 5&      �� 5'      .P�5(      ��  �� Width$
 Auto��6)   $��Price��66    �� DisplayMemberBinding 6E   �� *
PriceValue+�� 5)      $
 Auto��6)   $��Pricing Model��66    6M   �� *PricingModel+�� 5*      $
 Auto��6)   $��Option Type��66    6K   �� *OptionObj.OptionType+�� 5+      $
 Auto��6)   $��Maturity��66    6H   �� *OptionObj.Maturity+�� 5,      $
 Auto��6)   $��Risk Free Rate��66    6N   �� *OptionObj.RiskFreeRate+�� 5-      $
 Auto��6)   $��Underlying Name��66    6O   �� *(&OptionObj.UnderlyingObj.UnderlyingName+�� 5.      $
 Auto��6)   $��Underlying Type��66    6O   �� *(&OptionObj.UnderlyingObj.UnderlyingType+�� 5/      $
 Auto��6)   $��Underlying Spot��66    6O   �� *OptionObj.UnderlyingObj.Spot+�� 50      $
 Auto��6)   $��Underlying Volatility��66    6U   �� *$"OptionObj.UnderlyingObj.Volatility+�� 51      $��Delete��6)   $	 200��69   	 �� CellTemplate	 52      �� 53   "   .R�54   &   �� -   5        $��Delete��54   -   ��6U   �� 55   #   57      58      5:      �� 5=      $��1��5>   !   $��10 0 0 0q�5?   !   
 �� Orientation$
 
Horizontal=�5@   !   .+�5A      �� $��Refresh��5C   %   ���#FE6584
���������5B   %   �� *RefreshCommand+5E   %   5F      5G      5H      5I      