using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;

namespace WPB.ReactiveSpellChecker
{
    public class Checker : IDisposable
    {
        
        public class Result
        {
            public string Word { get; set; }
            public bool IsCorrect { get; set; }
            public IEnumerable<string> Suggestions { get; set; } 
        }


        private readonly Hunspell _hunspell;
        

        public Checker()
        {
            _hunspell = new Hunspell("Dictionary/en_US.aff", "Dictionary/en_US.dic");
        }

        public IEnumerable<Result> Check(string wordOrSentence)
        {
            var rval = new List<Result>();
            //clean up words
            var words = wordOrSentence.Trim().Replace(",","").Replace(".","").Replace("?","").Replace("\"","").Replace("'","").Split(' ');
            //check and get suggestsions
            foreach (var w in words)
            {
                var resSug = new List<string>();
                //check
                var res = new Result()
                {
                    Word = w,
                    IsCorrect = _hunspell.Spell(w),
                    Suggestions = resSug
                };

                //if invalid give some options
                if(!res.IsCorrect)
                    resSug.AddRange(_hunspell.Suggest(w));

                rval.Add(res);
            }
            return rval;
        }

        public void Dispose()
        {
            if(!_hunspell.IsDisposed)
                _hunspell.Dispose();
        }
    }
}
