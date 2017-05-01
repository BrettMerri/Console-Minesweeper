using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleMinesweeper
{
    class Timer
    {
        public Action<object, ElapsedEventArgs> Elapsed { get; internal set; }
    }
}
