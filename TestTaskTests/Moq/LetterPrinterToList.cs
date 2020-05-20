using System.Collections.Generic;
using System.Linq;
using TestTask;
using TestTask.LetterPrinters;

namespace TestTaskTests
{
    public class LetterPrinterToList : ILetterPrinter
    {
        public List<LetterStats> lettersPrintered;
        public void Print(IEnumerable<LetterStats> letters)
        {
            lettersPrintered = letters.ToList();
        }
    }
}
