using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace VirtualTimeMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //get vbox vms
            RefreshVBOXlist();
            //add names to os combo box
            foreach (string name in osnames)
            {
                cboOS.Items.Add(name);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //locate vmc file using open file dialog
            Microsoft.Win32.OpenFileDialog opendiag = new Microsoft.Win32.OpenFileDialog();
            opendiag.DefaultExt = ".vmc";
            opendiag.Filter = "Virtual Machine Config file (.vmc)|*.vmc";
            Nullable<bool> result = opendiag.ShowDialog();

            if (result == true)
            {
                //load filename
                txtVMC.Text = opendiag.FileName;
                getinfo();
            }
        }

        private void btnVpcOpen_Click(object sender, RoutedEventArgs e)
        {
            if (txtVMC.Text.Length > 3)
            {
                getinfo();
            }
            else
            {
                MessageBox.Show("Enter the location of a vmc file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string VMCxml;

        private void getinfo()
        {
            //load VMC info


                string date = "";
                TextReader tr = new StreamReader(txtVMC.Text);            
                VMCxml = tr.ReadToEnd();
                tr.Close();
                if (!VMCxml.Contains("<time_bytes type=\"bytes\">"))
                {
                    MessageBox.Show("The VMC file does not contain a date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string vmcdate = Regex.Split(VMCxml, "<time_bytes type=\"bytes\">")[1].Split('<')[0];
                    date = vmcdate.Substring(14, 2) + ",";
                    date = date + vmcdate.Substring(16, 2) + ",";

                    try
                    {
                        string[] cent = Regex.Split(VMCxml, "<cmos type=\"bytes\">");
                        date = date + cent[1].Substring(72, 2);
                    }

                    catch { }
                    date = date + vmcdate.Substring(18, 2);

                    DateTime dt;
                    if (DateTime.TryParse(date, out dt))
                    {
                       // datepickVPC.SelectedDate = dt;
                        txtVpcDate.Text = dt.ToLongDateString();
                    }
                    else
                    {
                        MessageBox.Show("Unable to parse date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                   


            
        }

        private void btnVpcSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dday = datepickVPC.SelectedDate.Value.Day.ToString();
                if (dday.Length == 1)
                {
                    dday = "0" + dday;
                }
                string dm = datepickVPC.SelectedDate.Value.Month.ToString();
                if (dm.Length == 1)
                {
                    dm = "0" + dm;
                }

                string[] centdate = Regex.Split(VMCxml, "<time_bytes type=\"bytes\">");
                VMCxml = centdate[0];
                VMCxml += "<time_bytes type=\"bytes\">";
                VMCxml += centdate[1].Substring(0, 14);
                VMCxml += dday + dm + datepickVPC.SelectedDate.Value.Year.ToString().Substring(2);
                VMCxml += centdate[1].Substring(20);

                string[] cent = Regex.Split(VMCxml, "<cmos type=\"bytes\">");
                VMCxml = cent[0];
                VMCxml += "<cmos type=\"bytes\">";
                VMCxml += cent[1].Substring(0, 72);
                VMCxml += datepickVPC.SelectedDate.Value.Year.ToString().Substring(0, 2);
                VMCxml += cent[1].Substring(74);

                TextWriter tw = new StreamWriter(txtVMC.Text, false, Encoding.Unicode);

                if (!VMCxml.Contains("<host_time_sync>"))
                {
                    string[] vmc = Regex.Split(VMCxml, "</mouse>");
                    tw.Write(vmc[0]);
                    tw.WriteLine("</mouse>");
                    tw.WriteLine("<components>");
                    tw.WriteLine("<host_time_sync>");
                    tw.WriteLine("<enabled type=\"boolean\">false</enabled>");
                    tw.WriteLine("</host_time_sync>");
                    tw.WriteLine("</components>");
                    tw.Write(vmc[1]);
                }
                else
                {
                    string[] vmxmlstrings = Regex.Split(VMCxml, "<host_time_sync>");
                    tw.Write(vmxmlstrings[0]);
                    tw.WriteLine("<host_time_sync>");
                    tw.WriteLine("<enabled type=\"boolean\">false</enabled>");
                    tw.WriteLine("</host_time_sync>");
                    tw.Write(Regex.Split(vmxmlstrings[1], "</host_time_sync>")[1]);

                }


                tw.Close();
                lblStatus.Content = "Updated Virtual PC date";
               // MessageBox.Show("Saved Virtual Machine Config File successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Unknown Error. Unable to save VMC.", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
                lblStatus.Content = "Unknown Error. Unable to save VMC.";
            }
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            //starts virtual machine
            try
            {
                Process pr = new Process();
                pr.StartInfo.FileName = txtVPCexe.Text;
                pr.StartInfo.Arguments = "-startvm \"" + txtVMC.Text + "\"";
                pr.Start();
            }
            catch
            {
                try
                {
                    Process pr = new Process();
                    pr.StartInfo.FileName = "C:\\Program Files (x86)\\Microsoft Virtual PC\\Virtual PC.exe";
                    pr.StartInfo.Arguments = "-startvm \"" + txtVMC.Text + "\"";
                    pr.Start();
                }
                catch
                {
                MessageBox.Show("Error starting Virtual PC 2007", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private List<string> vboxinfolist = new List<string>();
        private List<string> vboxinfofilelist = new List<string>();
        private List<string> vboxnamelist = new List<string>();

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshVBOXlist();
        }


 

        private void RefreshVBOXlist()
        {
            //run vboxmanage and get a list of VMs
            vboxnamelist.Clear();
            vboxinfolist.Clear();
            vboxinfofilelist.Clear();
            cmbVbVms.Items.Clear();
            Process pr = new Process();
            pr.StartInfo.FileName = txtVboxManageEXEloc.Text;
            pr.StartInfo.Arguments = "list  vms --long";
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.OutputDataReceived += new DataReceivedEventHandler(ConsoleOutputHandler);
            pr.StartInfo.CreateNoWindow = true;
            pr.Start();
            pr.BeginOutputReadLine();
        }

        private void ConsoleOutputHandler(object sendingProcess, DataReceivedEventArgs recieved)
        {
            if (!String.IsNullOrEmpty(recieved.Data))
            {
                    if (recieved.Data.Contains("Name:"))
                    {
                        string r = recieved.Data;
                        r = r.Substring(17);

                        //add name to combobox, using dispacher as we are in a different thread...
                        cmbVbVms.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Windows.Threading.DispatcherOperationCallback(delegate
                        {
                            cmbVbVms.Items.Add(r);
                            return null;

                                                    }), null);


                    }
                    if (recieved.Data.Contains("Time offset:"))
                    {
                        string r = recieved.Data;
                        r = r.Substring(17);
                        r = r.Split(' ')[0];
                        vboxinfolist.Add(r);

                    }

                    if (recieved.Data.Contains("Config file:"))
                    {
                        string r = recieved.Data;
                        r = r.Substring(17);
                        vboxinfofilelist.Add(r);
                    } 
            }
        }

        private void cmbVbVms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //add an amount of milliseconds to todays date to display vm's date

            try
            {
                DateTime vboxmil = DateTime.Today;
                vboxmil = vboxmil.AddMilliseconds(double.Parse(vboxinfolist[cmbVbVms.SelectedIndex]));
                txtOldVBdate.Text = vboxmil.ToLongDateString();
              //  dtpVBox.SelectedDate = vboxmil;
              }
            catch
            {
                //MessageBox.Show("Error in loading VM date info. If you clicked refresh, please ignore this", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtOldVBdate.Text = "Probably today";
            }

        }

        private void btnSaveVBVM_Click(object sender, RoutedEventArgs e)
        {
            //VBOX date offset


            DateTime dt = dtpVBox.SelectedDate.Value;
            string vmname = cmbVbVms.Text;
            TimeSpan dtdays = dt - DateTime.Today;
            double ms = dtdays.TotalMilliseconds;
            Process pr = new Process();
            pr.StartInfo.FileName = txtVboxManageEXEloc.Text;
            pr.StartInfo.Arguments = "modifyvm \"" + vmname + "\" --biossystemtimeoffset " + ms.ToString();
            pr.Start();
            pr.WaitForExit();

            lblStatus.Content = "Date changed for " + vmname;
           // MessageBox.Show("Date updated.", "Date Changed",MessageBoxButton.OK,MessageBoxImage.Information);
            
        }

        private void btnRunVBOX_Click(object sender, RoutedEventArgs e)
        {
            //starts virtual machine
            Process pr = new Process();
            pr.StartInfo.FileName = vboxinfofilelist[cmbVbVms.SelectedIndex];
            pr.Start();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboBuild.Items.Clear();

            if (e.AddedItems.Count == 1)
            {
                string os = e.AddedItems[0].ToString();                 
                for (int i = 0; i <= buildskeys.GetUpperBound(0); i++)
                {
                    if (os == buildskeys[i, 0])
                    {
                        cboBuild.Items.Add(buildskeys[i, 1]);
                    }
                }
            }
            else
            {
            }


        }

        string[] osnames = new string[] { "Neptune", "XP", "Longhorn", "Vista (post reset)", "7" };

        string[,] buildskeys = new string[,]{   {"Longhorn", "3683", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "23.09.02"},
                                                {"Longhorn", "3706", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "29.10.02"},
                                                {"Longhorn", "3718", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "19.11.02"},
                                                {"Longhorn", "4008", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "19.02.03"},
                                                {"Longhorn", "4011", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "05.03.03"},
                                                {"Longhorn", "4015", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "28.03.03"},
                                                {"Longhorn", "4028", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "01.07.03"},
                                                {"Longhorn", "4029", "CKY24-Q8QRH-X3KMR-C6BCY-T847Y", "19.06.03"},
                                                {"Longhorn", "4033", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "17.07.03"},
                                                {"Longhorn", "4051", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "01.10.03"},
                                                {"Longhorn", "4053", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "22.10.03"},
                                                {"Longhorn", "4066", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "17.02.04"},
                                                {"Longhorn", "4074", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "25.04.04"},
                                                {"Longhorn", "4083", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "16.05.04"},
                                                {"Longhorn", "4093", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "19.08.04"},
                                                {"Longhorn", "5048", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "02.04.05"},
                                                {"Vista (post reset)", "5112", "TCP8W-T8PQJ-WWRRH-QH76C-99FBW", "21.07.05"},
                                                {"Vista (post reset)","5219", "GKFV7-F2D9H-QKYXY-777P3-4M73W", "31.08.05"},
                                                {"Vista (post reset)","5231", "GKFV7-F2D9H-QKYXY-777P3-4M73W", "13.09.05"},
                                                {"Vista (post reset)","5231 x64", "R4HB8-QGQK4-79X38-QH3HK-Q3PJ6", "05.10.05"},
                                                {"Vista (post reset)","5259", "TGX39-HB48W-R29DH-6BVKB-3XFDW", "14.11.05"},
                                                {"Vista (post reset)","5270", "R4HB8-QGQK4-79X38-QH3HK-Q3PJ6", "15.12.05"},
                                                {"Vista (post reset)","5308", "7KYMQ-R788Q-4RF69-KTWKM-92PFJ", "18.02.06"},
                                                {"7", "7000", "No key needed", "13.12.08"},
                                                {"7","7100", "No key needed", "22.04.09"},
                                                {"Neptune", "5111", "W7XTC-2YWFB-K6BPT-GMHMV-B6FDY", "11.12.1999"},
                                                {"XP", "2202", "No key needed", "03.02.2000"},
                                                {"XP", "2211", "No key needed", "10.03.2000"},
                                                {"XP", "2223", "No key needed", "12.04.2000"},
                                                {"XP", "2250", "No key needed", "29.06.2000"},
                                                {"XP", "2257", "No key needed", "11.08.2000"},};


        private void cboBuild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                string build = e.AddedItems[0].ToString();
                for (int i = 0; i <= buildskeys.GetUpperBound(0); i++)
                {
                    if (build == buildskeys[i, 1])
                    {
                        txtProductKey.Text = buildskeys[i, 2];
                        DateTime dt = DateTime.Parse(buildskeys[i, 3]);
                        dtpVBox.SelectedDate = dt;
                        datepickVPC.SelectedDate = dt;
                    }
                }
            }
            else
            {
            }

            

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //open MCB
            Process pr = new Process();
            pr.StartInfo.FileName = "http://www.microsoftcollectionbook.com/";
            pr.Start();
        }


        
      }
}
