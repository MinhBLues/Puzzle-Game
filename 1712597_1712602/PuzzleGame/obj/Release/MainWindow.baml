   M S B A M L   `   `   `  ���� E  APuzzleGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PuzzleGame
PuzzleGame      PuzzleGame.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:PuzzleGame     -     � Title$  Puzzle Game��$	��600��$	��940�� � WindowStartupLocation$ CenterScreen=� � 	Resources 0   *PuzzleGame.RelativeToAbsolutePathConverter   	converter��%   &	          .��� -   $��gridView��.+�]� .+�`� $��40��$��0,0,0,10q�$	��Top=� `� 
Background
 �����.�c� 
�������.@��� �� -   $��0,2,5,0q�.����  �� Orientation$ 
Horizontal=�.+��� $��20��$��20�� �� BitmapScalingMode$ HighQuality=����� *	icon[0]+ �� 	Converter# [  �� $��10,2,0,0q�Save�� o� $��0,2,5,0q�
�������.@�m� $
��Load�� m� Icon �� $��20��$��20��$ HighQuality=����� *	icon[1]+# [  .@�m� -   $��	Load Game�� �� $��20��$��20��$ HighQuality=����� *	icon[2]+# [  m� -   $��Load Images�� �� $��20��$��20��$ HighQuality=����� *	icon[3]+# [  �� o� $��0,2,5,0q�
�������.@�m� $��Options�� �� $��20��$��20��$ HighQuality=����� *	icon[4]+# [  .@�	� -   $��	btnCheck1��.��3 * 3	� -   $��	btnCheck2��.��6 * 6	� -   $��	btnCheck3��.��9 * 9�� o� $��0,2,5,0q�
�������.@�m� $
��Help�� �� $��20��$��20��$ HighQuality=����� *	icon[5]+# [  .@��� -	   $��tbHelp��$��10,2,0,0q�� $
��Left=�.+��� -
   $��gbPuzzleBox��$��	PuzzleBox��$��0,0,10,0q�$	��600��.���� -   $��
canvasGame��
�������$��0,0,0,0q�$
��Left=�� $��Right=�.+��� $��Original��$	��Top=�$	��300��$	��250��$	��Top=�.���� -   $��Original��$ HighQuality=��� $��Bottom=�.+��� -   $��lblTime��$��30��$	��100��$��20(�
���  ��$
��Bold"�$��10,0,0,0q�.��
00:00:00�� -   $��lblMove��$��30��$	��150��$��15(�
�������$��10,0,0,0q�$��Medium"�$��10,0,0,0q�.��Moves Made: 0�� $ 
Horizontal=�$��Center=�$��
20,10,0,10q�.+��� -   
�������
�������$��90��$��40��$��0,0,10,0q�.���� $ 
Horizontal=�.+��� $��20��$��20��$ HighQuality=����� *	icon[6]+# [  �� $��10,2,0,0q�	Shuffle�� -   
�������
�������$��90��$��40��$��0,0,10,0q�.���� $ 
Horizontal=�.+��� -   $��imgPlay��$��20��$��20��$ HighQuality=����� *	icon[7]+# [  �� -   $��btnPlay��$��10,2,0,0q�Play�� -   
�������
�������$��90��$��40��$��0,0,10,0q�.���� $ 
Horizontal=�.+��� $��20��$��20��$ HighQuality=����� *	icon[9]+# [  �� $��10,2,0,0q�Quit�� $ Vertical=�.+��� -   
�������$��0q�$	��Top=�$��40��$��40��.���� $ HighQuality=����� *
icon[10]+# [  �� $	��120��$ 
Horizontal=�$��Center=�.+��� -   
�������$��0q�$��40��$��40��.���� $ HighQuality=����� *
icon[11]+# [  �� -   
�������$��0q�$��40,0,0,0q�$��40��$��40��.���� $ HighQuality=����� *
icon[12]+# [  �� -   
�������$��0q�$��Bottom=�$��40��$��40��.���� $ HighQuality=����� *
icon[13]+# [  