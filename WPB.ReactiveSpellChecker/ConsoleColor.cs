using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPB.ReactiveSpellChecker
{
    public class ConsoleColor : IDisposable
    {
        private readonly System.ConsoleColor _previousColor;

        public ConsoleColor(System.ConsoleColor color)
        {
            _previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }



        public void Dispose()
        {
            Console.ForegroundColor = _previousColor;
        }
    }
}
