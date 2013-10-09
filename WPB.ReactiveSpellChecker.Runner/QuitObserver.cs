using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WPB.ReactiveSpellChecker.Runner
{
    public class QuitSubject : ISubject<string>, IDisposable
    {
        readonly string[] quitWords = new string[] {"moist","dude","slacks"}; 

        public void OnCompleted()
        {
            //do nothing
        }

        public void OnError(Exception error)
        {
            Console.Error.WriteLine(error);
        }

        public void OnNext(string value)
        {
            //check for the words
            var words = value.Trim().Replace(",", "").Replace(".", "").Split(' ');
            var truth = words.Any(x => 
                quitWords.Any(
                    y => String.Equals(x, y, StringComparison.InvariantCultureIgnoreCase)
                    )
                );

            if (!truth) return;
            
            foreach(var x in _observers)
                x.OnNext(value);
        }

        readonly ICollection<IObserver<string>> _observers = new Collection<IObserver<string>>(); 

        public IDisposable Subscribe(IObserver<string> observer)
        {
            _observers.Add(observer);
            return this;
        }



        public void Dispose()
        {
            foreach(var x in _observers)
                x.OnCompleted();

            _observers.Clear();
        }
    }
}
