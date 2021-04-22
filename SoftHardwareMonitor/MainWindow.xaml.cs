using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.Diagnostics;

namespace SoftHardwareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer idomero = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            idomero.Tick += new EventHandler(Idozito_Tick);
            idomero.Interval = new TimeSpan(0, 0, 1);
            idomero.Start();

        }

        private void Idozito_Tick(object sender, EventArgs e)
        {
            GetTime();
        }

        private void GetTime()
        {
            DateTime date;
            date = DateTime.Now;
            Ora.Text = date.ToLongTimeString() + "  " + date.ToLongDateString();
        }
        private void GetGPUinfo()
        {
            ManagementClass wmi = new ManagementClass("Win32_VideoController");
            var providers = wmi.GetInstances();

            foreach (var provider in providers)
            {
                string Gpuname = provider["Name"].ToString();
                string DeviceID = provider["DeviceID"].ToString();
                string Driververs = provider["DriverVersion"].ToString();
                
                string Videomemotype = provider["VideoMemoryType"].ToString();
                GPU_Name.Text = "Videó kártya neve: " + "" + Gpuname.ToString();
                GPU_DeviceID.Text = "Videó kártya ID: " + "" + DeviceID.ToString();
                GPU_Driverversion.Text = "Videó kártya Driver verzó: " + "" + Driververs.ToString();
                
                GPU_VideoMemoryType.Text = "Videó kártya Memória: " + "" + Videomemotype.ToString();
            }
        }
        private void GetOpsysteminfo()
        {
            ManagementClass wmi = new ManagementClass("Win32_OperatingSystem");
            var providers = wmi.GetInstances();

            foreach (var provider in providers)
            {
                string Systemversion = provider["Version"].ToString();
                string Systemname = provider["Caption"].ToString();
                string Systemtype = provider["OSType"].ToString();
                string sysdirectory = provider["SystemDirectory"].ToString();
                SystemTypetb.Text = "Operációs Rendszer verzió: " + "" + Systemversion.ToString();
                SystemName.Text = "Operációs Rendszer Neve: " + "" + Systemname.ToString();
                SystemType.Text = "Operációs Rendszer típusa: " + "" + Systemtype.ToString();
                Systemdirectory.Text = "Operációs Rendszer helye: " + "" + sysdirectory.ToString();
            }
        }

        private void GetProcessorInfo()
        {
            ManagementClass wmi = new ManagementClass("Win32_Processor");
            var providers = wmi.GetInstances();

            foreach (var provider in providers)
            {
                int Family = Convert.ToInt16(provider["Family"]);
                int CpuClockspeed = Convert.ToInt32(provider["CurrentClockSpeed"]);
                string cpuStatus = Convert.ToString(provider["Status"]);
                string cpuname = Convert.ToString(provider["Name"]);
                string cpudivid = Convert.ToString(provider["DeviceID"]);
                CPU_Status.Text = "CPU Státusz: " + "" + cpuStatus;
                CPU_Clockspeed.Text = "CPU Current Clockspeed:" + "" + CpuClockspeed.ToString();
                CPU_Family.Text = "CPU Család:" + "" + Family.ToString();
                CPU_Name.Text = "CPU Név:" + "" + cpuname.ToString();
                CPU_Device_ID.Text = "CPU ID:" + "" + cpudivid.ToString();
                CPUclockspeed.Value = CpuClockspeed;
            }
        }

        private void CPU_Check_Click(object sender, RoutedEventArgs e)
        {
            GetProcessorInfo();
        }

        private void Op_Check_Click(object sender, RoutedEventArgs e)
        {
            GetOpsysteminfo();
        }

        private void GPU_Check_Click(object sender, RoutedEventArgs e)
        {
            GetGPUinfo();
        }

        private void SoftwareTab_Click(object sender, RoutedEventArgs e)
        {
            Window Softwareablak = new Window();
            Softwareablak.Show();
        }
    }
}
