using System;
using TestTask.LetterPrinters;

namespace TestTask.Analyz
{
    public class LetterAnalyzer
    {
        private readonly IReadOnlyStream _stream;

        public LetterAnalyzer(IReadOnlyStream stream)
        {
            _stream = stream;
        }

        public void PrintSingleLetterStatistic(ILetterPrinter letterPrinter, CharType charType)
        {
            throw new NotImplementedException();
        }

        public void PrintDoubleLetterStatistic(ConsolePrinter consolePrinter, CharType charType)
        {
            throw new NotImplementedException();
        }
    }
}
