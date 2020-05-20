using System.Collections.Generic;

namespace TestTask.LetterPrinters
{
    public interface ILetterPrinter
    {
        void Print(IEnumerable<LetterStats> letters);
    }
}
