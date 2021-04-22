using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftHardwareMonitor
{
    class Softwaretarolo
    {
        public string Nev { get; set; }
        public string Verzio { get; set; }
        public string Ujabb { get; set; }

        public Softwaretarolo(string sor)
        {
            string[] sorElem = sor.Split(',');
            Nev = sorElem[0];
            Verzio = sorElem[1];
            Ujabb = "Unknown";
        }
    }
}
