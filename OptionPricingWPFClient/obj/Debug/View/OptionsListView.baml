   M S B A M L   `   `   `  ����Q  MOptionPricingWPFClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:OptionPricingWPFClient.ViewOptionPricingWPFClient.View  5      S.clr-namespace:OptionPricingWPFClient.ViewModel OptionPricingWPFClient.ViewModel  5      1    +OptionPricingWPFClient.View.OptionsListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:OptionPricingWPFClient.View     5      D	viewModel.clr-namespace:OptionPricingWPFClient.ViewModel     5      ���#0F0F2D  � 	Resources  5         HeaderStyle �� 
TargetType �� Property �� Value��%|   &	        �� 5   
   " ��6$   .�5      ��  � �� �#FE65846+   �� 5       � ��$ 24(�6)   5      5      .��5      �� $
��Auto=�6   
������ .��5   
   �  � RowDefinitions 5      �� 5      $��50 �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� #��[  6   $��0��5   !   $��All options��5    !   $��10 0 0 0q�5!   !   �� 5#      -   5        $��	listView1��5#   q   $��	10 10 0 0q�6   $��4��6V   $
��auto��6c   $
��auto��6�   ���#FE6584���#0F0F2D��6�   �� *ItemsCollection+ �� View 5%      �� 5&      .P�5'      ��  �� Width$
 Auto��6)   $��Option Type��66    �� DisplayMemberBinding 6K   �� *
OptionType+�� 5(      $
 Auto��6)   $��Maturity��66    6H   �� *
Maturity+�� 5)      $
 Auto��6)   $��Risk Free Rate��66    6N   �� *RiskFreeRate+�� 5*      $
 Auto��6)   $��Underlying Name��66    6O   �� *UnderlyingObj.UnderlyingName+�� 5+      $
 Auto��6)   $��Underlying Type��66    6O   �� *UnderlyingObj.UnderlyingType+�� 5,      $
 Auto��6)   $��Underlying Spot��66    6O   �� *UnderlyingObj.Spot+�� 5-      $
 Auto��6)   $��Underlying Volatility��66    6U   �� *UnderlyingObj.Volatility+5.      52      �� 54      $��1��55   !   $��10 0 0 0q�56   !    �� Orientation$ 
Horizontal=�57   !   .+�58      �� $��Refresh��5:   %   ���#FE6584
���������59   %   �� *RefreshCommand+5<   %   5=      5>      5?      5@      