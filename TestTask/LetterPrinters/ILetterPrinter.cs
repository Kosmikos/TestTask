using System.Collections.Generic;

namespace TestTask.LetterPrinters
{
    public interface ILetterPrinter
    {
        void PrintStatistic(IEnumerable<LetterStats> letters);
    }
}
