using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
    class Class1
    {
        public void S2()
        {
            S1();
        }
        virtual public void S1()
        {
            Console.WriteLine("F: S1");
        }
    }
}
