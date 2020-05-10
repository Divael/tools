using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.serialPort
{
    public class Com
    {
        SerialPort port;
        public Com() {
            port = new SerialPort();
            
        }

    }
}
