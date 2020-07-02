using System.IO.Ports;

namespace Comport
{
    public class ComHandled : Com
    {
        public ComHandled(ComPortParm cpp) : base(cpp)
        {
        }

        public byte[] Read()
        {
            byte[] array = null;
            SerialPort handle = this.handle;
            lock (handle)
            {
                array = new byte[this.handle.BytesToRead];
                this.handle.Read(array, 0, array.Length);
            }
            return array;
        }
    }
}
