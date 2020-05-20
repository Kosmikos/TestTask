using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TestTask;
using TestTask.Analyz;

namespace TestTaskTests
{
    [TestClass]
    public class LetterAnalyzerTests
    {
        [TestMethod]
        public void PrintSingleLetterStatistic_Eng()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("aicaiiBifB");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('a', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);

            oneLetter = printer.lettersPrintered[1];
            Assert.AreEqual('i', oneLetter.Letter);
            Assert.AreEqual(4, oneLetter.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_EngTwoReadStream()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("aicaiiBifB");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('a', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);

            oneLetter = printer.lettersPrintered[1];
            Assert.AreEqual('i', oneLetter.Letter);
            Assert.AreEqual(4, oneLetter.Count);

            //second read
            analyzer.PrintSingleLetterStatistic(printer, CharType.Vowel);

            oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('a', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);

            oneLetter = printer.lettersPrintered[1];
            Assert.AreEqual('i', oneLetter.Letter);
            Assert.AreEqual(4, oneLetter.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_EngRemoveNotVowel()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("aicaiiBifB");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual(2, printer.lettersPrintered.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_SkipNumber()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("aica123");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Consonants);

            Assert.AreEqual(1, printer.lettersPrintered.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_EngRemoveNotConsonants()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("aicaiiBifB");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Consonants);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual(3, printer.lettersPrintered.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_EngUseRegister()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("aicaiiBifb");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Consonants);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual(4, printer.lettersPrintered.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_RusConsonants()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("ПриведМедвед");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Consonants);

            var oneLetter = printer.lettersPrintered[3];
            Assert.AreEqual('д', oneLetter.Letter);
            Assert.AreEqual(3, oneLetter.Count);
        }

        [TestMethod]
        public void PrintSingleLetterStatistic_RusVowel()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("ПриведМедвед");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintSingleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[1];
            Assert.AreEqual('е', oneLetter.Letter);
            Assert.AreEqual(3, oneLetter.Count);
        }

        [TestMethod]
        public void PrintDoubleLetterStatistic_EngVowel()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("HOOelloWood");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintDoubleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('O', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);
        }

        [TestMethod]
        public void PrintDoubleLetterStatistic_EngOneCharInStream()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("H");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintDoubleLetterStatistic(printer, CharType.Vowel);

            Assert.AreEqual(0, printer.lettersPrintered.Count);
        }

        [TestMethod]
        public void PrintDoubleLetterStatistic_RusVowel()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("ДвойныыеБууквыУУу");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintDoubleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('Ы', oneLetter.Letter);
            Assert.AreEqual(1, oneLetter.Count);

            oneLetter = printer.lettersPrintered[1];
            Assert.AreEqual('У', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);
        }

        [TestMethod]
        public void PrintDoubleLetterStatistic_3Letter()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("УУу");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintDoubleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('У', oneLetter.Letter);
            Assert.AreEqual(1, oneLetter.Count);
        }

        [TestMethod]
        public void PrintDoubleLetterStatistic_4Letter()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("УУуУ");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintDoubleLetterStatistic(printer, CharType.Vowel);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('У', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);
        }

        [TestMethod]
        public void PrintDoubleLetterStatistic_SkipNumber()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("шшШШ66333");
            writer.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            var reader = new ReadOnlyStream(ms);
            var analyzer = new LetterAnalyzer(reader);
            var printer = new LetterPrinterToList();
            analyzer.PrintDoubleLetterStatistic(printer, CharType.Consonants);

            var oneLetter = printer.lettersPrintered[0];
            Assert.AreEqual('Ш', oneLetter.Letter);
            Assert.AreEqual(2, oneLetter.Count);
            Assert.AreEqual(1, printer.lettersPrintered.Count);
        }
    }
}
