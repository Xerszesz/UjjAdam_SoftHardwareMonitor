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
    }
}
