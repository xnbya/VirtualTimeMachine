   M S B A M L   `   `   `  ����   VirtualTimeMachine#    VirtualTimeMachine.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Virtual Time Machine��$	��277��$	��743�� � WindowStyle$ SingleBorderWindow=� � 
ResizeMode$ CanMinimize=� � SizeToContent$ WidthAndHeight=� � Icon$5 //VirtualTimeMachine;component/Images/Icon_2.ico��.��� $	��186��$	��666��.+��� -   $��tabControl1��$	��159��$
��Left=�$	��Top=�$	��664��$��1,1,0,0q�.@��� -   $��tabItem1��$��
VirtualBOX��.��� .+��� -   $��label1��$��VBoxManage.exe location:��$��28��$
��Left=�$��6,5,0,0q�$	��Top=��� -   $��label2��$��Virtual Machine:��$��28��$
��Left=�$��	60,36,0,0q�$	��Top=��� -   $��txtVboxManageEXEloc��$��23��$
��Left=�$��	164,7,0,0q�$	��Top=�$	��403��$7��1C:\Program Files\Oracle\VirtualBox\VBoxManage.exe���� -   $��btnVBRefresh��$��Refresh��$��23��$
��Left=�$��	573,7,0,0q�$	��Top=�$��75���� -   $��cmbVbVms��$��23��$
��Left=�$��
164,36,0,0q�$	��Top=�$	��484��(  "System.Windows.Controls.DatePicker  -   $��dtpVBox��$
��Left=�$��
164,95,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$	��193���� -	   $��label7��$��	New Date:��$��28��$
��Left=�$��	90,94,0,0q�$	��Top=��� -
   $��txtOldVBdate��$��23��$
��Left=�{�.  $��
164,66,0,0q�$	��Top=�$	��193���� -   $��label6��$��	Old Date:��$��28��$
��Left=�$��	95,64,0,0q�$	��Top=��� -   $��btnSaveVBVM��$
��Save��$��23��$��Right=�$��
0,96,216,0q�$	��Top=�$��75���� -   $��
btnRunVBOX��$��Run VM��$��23��$
��Left=�$��
444,96,0,0q�$	��Top=�$��75���� -   $��tabItem2��$��Virtual PC 2007��.��� .+��� -   $��label3��$��	VMC File:��$��28��$
��Left=�$��41,6,0,0q�$	��Top=��� -   $��btnVpcLocate��$��	Locate...��$��23��$
��Left=�$��	573,7,0,0q�$	��Top=�$��75���� -   $��txtVMC��$��23��$
��Left=�$��	106,7,0,0q�$	��Top=�$	��461��  -   $��datepickVPC��$
��Left=�$��
258,63,0,0q�$	��193��$	��Top=�$
 Long=��� -   $��
btnVpcOpen��$
��Open��$��23��$
��Left=�$��
106,35,0,0q�$	��Top=�$��75���� -   $��
btnVpcSave��$
��Save��$��23��$
��Left=�$��
106,63,0,0q�$	��Top=�$��75���� -   $��lblDate��$��	Old Date:��$��28��$
��Left=�$��
187,33,0,0q�$	��Top=�$��65���� -   $��label4��$��	New Date:��$��28��$
��Left=�$��
187,62,0,0q�$	��Top=��� -   $��
txtVpcDate��$��23��$
��Left=�$��
258,35,0,0q�$	��Top=�$	��193��{�.  �� -   $��	txtVPCexe��$��23��$
��Left=�$��
106,92,0,0q�$	��Top=�$	��461��$:��4C:\Program Files\Microsoft Virtual PC\Virtual PC.exe���� -   $��label5��$��Virtual PC.exe:��$��28��$
��Left=�$��	15,90,0,0q�$	��Top=��� -   $��button1��$��Run VPC��$��23��$
��Left=�$��
573,92,0,0q�$	��Top=�$��75���� -   $��tabItem3��$��About��.��� .+��� -   $��label9��$��Virtual Time Machine��$��51��$
��Left=�$��6,5,0,0q�$	��Top=�$��32(�$	��309���� -   $��label10��$	��0.8��$��50��$��Right=�$��
0,40,587,0q�$	��Top=�$��32(�$��59���� -   $��label11��$��Contributers��$��28��$
��Left=�$��	382,6,0,0q�$	��Top=��� -   $��label12��$��© 2012 Alexis Enston��$��28��$
��Left=�$��6,82,0,0q�$	��Top=��� -    $��label13��$��Build Information: MCB��$��28��$
��Left=�$��
382,52,0,0q�$	��Top=��� -!   $��image1��$��43��$��Right=�$��	0,84,-7,0q�$
��None=�$	��Top=�$	��338��$6��0/VirtualTimeMachine;component/Images/banner1.png���� -"   $��label14��$ ��Ideas and testing: DeFacto��$��28��$
��Left=�$��
382,28,0,0q�$	��Top=��� -#   $��image2��$	��150��$
��Left=�$��
184,73,0,0q�$
��Fill=�$	��Top=�$	��200���� -$   $��	lblStatus��$��28��$��Right=�$��0,-2,4,0q�$	��Top=�$��RightToLeft=�$	��431���� -%   $��label8��$	��OS:��$��28��$
��Left=�$��	6,159,0,0q�$	��Top=��� -&   $��cboOS��$��23��$
��Left=�$��
34,161,0,0q�$	��Top=�$	��120������ �� -'   $��label15��$��Build:��$��28��$
��Left=�$��160,160,0,0q�$	��Top=��� -(   $��cboBuild��$��23��$
��Left=�$��205,161,0,0q�$	��Top=�$��83���� -)   $��label16��$
��Key:��$��28��$
��Left=�$��294,160,0,0q�$	��Top=��� -*   $��btnMCB��$
��More��$
��Left=�$��614,161,0,0q�$��51��$��23��$	��Top=��� -+   $��txtProductKey��$��23��$
��Left=�$��332,161,0,0q�$	��Top=�$	��275��$��Normal"�