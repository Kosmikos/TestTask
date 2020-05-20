using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTask;
using TestTask.Analyz;

namespace TestTaskTests
{
    [TestClass]
    public class LetterAnalyzerTests
    {
        [TestMethod]
        public void PrintSingleLetterStatistic_EngVowel()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("abcabb");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.Equals("a", oneLetter.Letter);
            Assert.Equals(2, oneLetter.Count);

        }
    }
}
