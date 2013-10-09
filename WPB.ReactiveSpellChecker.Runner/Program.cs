using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WPB.ReactiveSpellChecker.Runner
{
    class Program
    {
        private static IDisposable _spellSubscription;
        private static IDisposable _quitSubscription;

        private static bool IsRunning = true;

        static void Main(string[] args)
        {

            var fact = Observable.Defer(
                () => Observable.Start(
                        ()=>Console.ReadLine()
                    )
                ).Repeat();



            var connectable = fact.Publish();

            var spellobserver = new SpellingObserver();
            var quitSubject = new QuitSubject();
            //subscribe to check spelling
            _spellSubscription = connectable.Subscribe(spellobserver);
            //subscribe to check quittin time
            _quitSubscription = connectable.Subscribe(quitSubject);
            //subscribe to stop spin lock
            quitSubject.Subscribe(Program.OnQuit);
            connectable.Connect();
            while (IsRunning)
            {
            }
            _spellSubscription.Dispose();
            _quitSubscription.Dispose();
        }

        public static void OnQuit(string value)
        {
            IsRunning = false;
        }
    }
}
