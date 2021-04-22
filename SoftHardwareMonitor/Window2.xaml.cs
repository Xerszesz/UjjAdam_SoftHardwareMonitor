using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace SoftHardwareMonitor
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

            Betolt();
            SoftwareGrid.ItemsSource = softwarebinding;
        }
        
        BindingList<Softwaretarolo> softwarebinding = new BindingList<Softwaretarolo>();
        public void Betolt()
        {
            string uninstall = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(uninstall))
            {
                foreach (string item in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(item))
                    {
                        var nev = subkey.GetValue("DisplayName");
                        var vers = subkey.GetValue("DisplayVersion");
                        if (nev != null)
                        {
                            if (vers != null)
                            {
                                string nev1 = nev.ToString();
                                string vers1 = vers.ToString();
                                string sor = nev1 + "," + vers1;
                                softwarebinding.Add(new Softwaretarolo(sor));
                            }
                        }
                    }
                }
            }
            foreach (var item in softwarebinding)
            {
                SoftwareGrid.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfd = new OpenFileDialog();
            openfd.Filter = "CSV állományok (.csv)|*.csv";
            openfd.ShowDialog();
            softwarebinding.Clear();
            StreamReader sr = new StreamReader(openfd.FileName);
            while(!sr.EndOfStream)
            {
                string sor1 = sr.ReadLine();
                softwarebinding.Add(new Softwaretarolo(sor1));
            }
            sr.Close();
            SoftwareGrid.DataContext = softwarebinding;
            foreach (var item in softwarebinding.Skip(1))
            {
                SoftwareGrid.Items.Add(item);
            }
        }

        private void Exportbutton_Click(object sender, RoutedEventArgs e)
        {
            SoftwareGrid.SelectAllCells();
            SoftwareGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, SoftwareGrid);
            SoftwareGrid.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            SaveFileDialog mentes = new SaveFileDialog();
            mentes.Filter = "CSV állományok (.csv)|*.csv";
            mentes.ShowDialog();
            StreamWriter sw = new StreamWriter(mentes.FileName, false, Encoding.UTF8);
            foreach (var item in result)
            {
                sw.Write(item);
            }
            sw.Close();
        }
    }
}
