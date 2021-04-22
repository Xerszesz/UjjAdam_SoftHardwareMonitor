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

        private void GetOpsysteminfo()
        {
            ManagementClass wmi = new ManagementClass("Win32_ComputerSystem");
            var providers = wmi.GetInstances();

            foreach (var provider in providers)
            {
                string Systeminfo = provider["SystemType"].ToString();
                SystemTypetb.Text = "Operációs Rendszer típus: " + "" + Systeminfo.ToString();
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
                CPU_Status.Text = "CPU Státusz: " + "" + cpuStatus;
                CPU_Clockspeed.Text = "CPU Clockspeed:" + "" + CpuClockspeed.ToString();
                CPU_Family.Text = "CPU Család:" + "" + Family.ToString();
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
    }
}
