using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfraStructure
{
    public class Testje
    {
        private readonly ICounter _counter;

        public Testje(ICounter counter)
        {
            _counter = counter;
        }

        public void DoeIets()
        {
            _counter.Increment();
            _counter.Show();
             _counter.Increment();
            _counter.Show();
        }
    }
}