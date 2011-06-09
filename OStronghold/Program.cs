using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold
{
    class Program
    {
        static void Main(string[] args)
        {
            StrongholdClass aStronghold = new StrongholdClass();

            aStronghold.populate(10);
        }
    }
}
