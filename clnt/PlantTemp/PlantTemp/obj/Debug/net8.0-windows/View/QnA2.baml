   M S B A M L   `   `   `  ����D  @PlantTemp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:PlantTemp	PlantTemp  5          PlantTemp.View.QnA2   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:PlantTemp     5        N� Title$
  QnA2��5	      .��5      � ���	#FFF5EFE6 � RowDefinitions 5   
   �� 5      .+�5   
   � $��Center=�6   $��77��6,   $��0,17,0,0q�68   $	��Top=�6J   $	��930��6b    � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��1* �6#   .+�5      �� $��0��6   $��로그아웃��6%   $��Center=�64   $��77��6Q   $��Center=�6]   $	��180��6x   $��20(�6�   
���������6�   Z�  � 	Resources 5       �� 
TargetType �� Property �� Value��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 10��6;   5      5      ��5      �� 5       �� Color$ 	#FF82A284��6&    �� Opacity$	 0.8Q�68   5       �� 5!      $��1��6   $��식물정보조회��6%   $��Center=�66   $��77��6S   $��Center=�6_   $	��180��6z   $��20(�6�   
���������6�   Z�  5"      ��%_   '��       �� 5#      " ��6   .�5$      ��  �@��CornerRadius$ 10��6;   5%      5&      ��5'      �� 5(      $ 	#FF82A284��6&   $	 0.8Q�68   5*      �� 5+      $��2��6   $��통계 확인��6%   $��Center=�65   $��77��6R   $��Center=�6^   $	��180��6y   $��20(�6�   
���������6�   Z�  5,      ��%_   '��       �� 5-      " ��6   .�5.      ��  �@��CornerRadius$ 10��6;   5/      50      ��51      �� 52      $ 	#FF82A284��6&   $	 0.8Q�68   54      �� 55      $��3��6   $	��QnA��6%   $��Center=�63   $��77��6P   $��Center=�6\   $	��180��6w   $��20(�6�   
���������6�   Z�  56      ��%_   '��       �� 57      " ��6   .�58      ��  �@��CornerRadius$ 10��6;   59      5:      ��5;      �� 5<      $ 	#FF446A46��6&   $ 1Q�68   5>      �� 5?      $��4��6   $��실시간상담��6%   $��Center=�65   $��77��6R   $��Center=�6^   $	��180��6y   $��20(�6�   
���������6�   Z�  5@      ��%_   '��       �� 5A      " ��6   .�5B      ��  �@��CornerRadius$ 10��6;   5C      5D      ��5E      �� 5F      $ 	#FF82A284��6&   $	 0.8Q�68   5H      5I      �� 5K   
   ���	#FF446A46$��3q�6)   $��Center=�6=   $	��410��6Z   $��	0,234,0,0q�6g   $	��Top=�6z   $	��814��6�    5L      ��%_   '��       �� 5M      " ��6   .�5N      ��  �@��CornerRadius$ 20��67   5O      5P      ��5Q      �� 5R      $ White��6"   $	 0.5Q�60   ._�5T      �� $��QnA 게시판��6   $
��Left=�6&   $��68��6A   $	��Top=�6M   $	��206��6e   $��35(�6q   $
��Bold"�6   ���	#FF446A46$��7,7,0,0q�6�   5U      �� 5W   
   -   5        	 ��Name$	 Qu_Title��5W      $��질문 제목��6"   $
��Left=�62   $��34��6M   $��223,312,0,0q�6Y   $	��Top=�6n   $	��124��6�   $��24(�6�   $��Arial Black)�6�   
 J� RenderTransformOrigin$
 0.503,0.623&�6�   �� 5X   
   $��답변��6   $��Right=�6   $��34��69   $��0,312,257,0q�6E   $	��Top=�6Z   $��56��6r   $��24(�6}   $
 0.524,-0.281&�6�   $��Arial Black)�6�   �� 5Y   
   
������ ���	#FFCBDDCC �� CornerRadius$ 10��6B   $
��Left=�6T   $	��Top=�6o   $��145,357,0,0q�6�   $	��280��6�   $	��265��6�   ._�5Z      �� -   5         ��Name$ Qu_Text��5Z      $��질문 내용��6)   $
��Wrap=�66   $��16(�6J    �� Padding$ 10q�6X   $��0,-6,0,6q�6e   5[      �� 5\   
   
������ ���	#FFCBDDCC$ 10��6B   $��Right=�6T   $	��Top=�6p   $��0,357,145,0q�6�   $	��280��6�   $	��265��6�   ._�5]      �� $��답변 내용 입력��6    �� TextWrapping$
 Wrap=�6&   $��16(�6:   $��10q�6H   
������ $��0q�6n   $��0,-6,0,6q�6�   5^      �� 5_   
   -   5         ��Name$ Back_btn��5_      $��돌아가기��6#   $
��Left=�62   $��64��6M   $��714,657,0,0q�6Y   $	��Top=�6n   $	��168��6�   
�������$��20(�6�   $
��Bold"�6�   $��10,1,1,1q�6�   ��6�   Z�  5`      ��%_   '��       �� 5a      " ��6   .�5b      ��  �@��CornerRadius$ 10��65   5c      5d      ��5e      �� 5f      $ 	#FFCBA7B6��6"   $	 0.8Q�64   5h      �� 5i   
   -   5        $ 	Enter_btn��5i      $��입력하기��6$   $
��Left=�63   $��64��6N   $��590,657,0,0q�6Z   $	��Top=�6o   $	��168��6�   ���	#FFCBA7B6
�������$��20(�6�   $
��Bold"�6�   ��6�   Z�  5j      ��%_   '��       �� 5k      " ��6   .�5l      ��  �@��CornerRadius$ 10��65   5m      5n      5o      5p      5q      