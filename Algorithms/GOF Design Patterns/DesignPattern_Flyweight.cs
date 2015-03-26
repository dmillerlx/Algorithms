using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    


    public class DesignPattern_Flyweight
    {

        System.Collections.Hashtable ht = new System.Collections.Hashtable();

        public DesignPattern_Flyweight()
        {
            
        }
        
        public System.Drawing.Brush getBrush(System.Drawing.Color c)
        {
            if (ht.Contains(c))
            {
                System.Drawing.Brush ret = (System.Drawing.Brush)ht[c];
                return ret;
            }

            System.Drawing.Brush b = new System.Drawing.SolidBrush(c);
            ht.Add(c, b);
            return b;
        }



    }
}
