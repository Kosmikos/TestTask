using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.LetterPrinters;

namespace TestTask.Analyz
{
    class LetterAnalyzer
    {
        private readonly IReadOnlyStream _stream;

        public LetterAnalyzer(IReadOnlyStream stream)
        {
            _stream = stream;
        }

        internal void PrintSingleLetterStatistic(ILetterPrinter letterPrinter, CharType charType)
        {
            throw new NotImplementedException();
        }

        internal void PrintDoubleLetterStatistic(ConsolePrinter consolePrinter, CharType charType)
        {
            throw new NotImplementedException();
        }
    }
}
