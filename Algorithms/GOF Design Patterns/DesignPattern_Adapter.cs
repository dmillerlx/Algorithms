using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Adapter
    {

        public class Mouse
        {
            public void ConnectB()
            {
                Console.WriteLine("Sending signal to USB Adapter");
            }
        }

        public class USBAdapter
        {
            Mouse mouse = new Mouse();

            public void ConnectA()
            {
                mouse.ConnectB();
            }

        }

        public void Run()
        {
            USBAdapter usb = new USBAdapter();
            usb.ConnectA();
        }



    }
}
