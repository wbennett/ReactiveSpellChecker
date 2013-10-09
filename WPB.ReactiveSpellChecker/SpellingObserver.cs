using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPB.ReactiveSpellChecker
{
    public class SpellingObserver : IObserver<string>, IDisposable
    {
        readonly Checker _checker = new Checker();

        public void OnCompleted()
        {
            using (new ConsoleColor(System.ConsoleColor.DarkGreen))
            {
                Console.WriteLine("We are done here.");
            }
        }

        public void OnError(Exception error)
        {
            using (new ConsoleColor(System.ConsoleColor.Red))
            {
                Console.WriteLine();
                Console.WriteLine("=========");
                Console.WriteLine(error);
                Console.WriteLine("=========");
                Console.WriteLine();
            }
        }

        public void OnNext(string value)
        {
            var res = _checker.Check(value);
            foreach (var r in res.Where(x=>!x.IsCorrect))
            {
                using (new ConsoleColor(System.ConsoleColor.DarkYellow))
                {
                    Console.WriteLine(string.Format("Oops! {0} is misspelled. Here are some suggestions:",r.Word));
                }
                using (new ConsoleColor(System.ConsoleColor.Green))
                {
                    foreach (var s in r.Suggestions)
                    {
                        Console.WriteLine(string.Format("\t-\t{0}",s));
                    }
                }
            }
            Console.WriteLine();
        }

        public void Dispose()
        {
            _checker.Dispose();
        }
    }
}
