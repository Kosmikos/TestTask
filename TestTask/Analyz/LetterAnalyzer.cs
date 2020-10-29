using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и выводит коллекцию статистик вхождения каждой буквы.
        /// Статистика РЕГИСТРОЗАВИСИМАЯ!
        /// </summary>
        /// <param name="letterPrinter">Объект для вывода данных статистики</param>
        /// <param name="useOnlyThisCharType">Какие типы букв учитывать при подсчете</param>
        public void PrintSingleLetterStatistic(ILetterPrinter letterPrinter, CharType useOnlyThisCharType)
        {
            IList<LetterStats> singleLetterStats = FillSingleLetterStats();

            var filteredLetter = FilterCharStatsByType(singleLetterStats, useOnlyThisCharType);
            letterPrinter.PrintStatistic(filteredLetter);
        }

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и выводящая коллекцию статистик вхождения парных букв.
        /// В статистику должны попадать только пары из одинаковых букв, например АА, СС, УУ, ЕЕ и т.д.
        /// Статистика - НЕ регистрозависимая!
        /// </summary>
        /// <param name="letterPrinter">Объект для вывода данных статистики</param>
        /// <param name="useOnlyThisCharType">Какие типы букв учитывать при подсчете</param>
        public void PrintDoubleLetterStatistic(ILetterPrinter letterPrinter, CharType useOnlyThisCharType)
        {
            IList<LetterStats> singleLetterStats = FillDoubleLetterStats();

            var filteredLetter = FilterCharStatsByType(singleLetterStats, useOnlyThisCharType);
            letterPrinter.PrintStatistic(filteredLetter);
        }

        private IList<LetterStats> FillSingleLetterStats()
        {
            var dictLetter = new Dictionary<char, LetterStats>();
            _stream.ResetPositionToStart();
            while (!_stream.IsEof)
            {
                char c = _stream.ReadNextChar();
                if (!char.IsLetter(c))
                    continue;

                if (!dictLetter.ContainsKey(c))
                {
                    dictLetter[c] = new LetterStats { Letter = c };
                }

                dictLetter[c].IncStatistic();
            }

            return dictLetter.Values.ToList();
        }

        private IList<LetterStats> FillDoubleLetterStats()
        {
            char prevChar = default;
            // в ТЗ не указано последовательность ААА это одно двойное входждение или два. Считаем что одно
            var dictLetter = new Dictionary<char, LetterStats>();
            _stream.ResetPositionToStart();
            while (!_stream.IsEof)
            {
                char currentChar = _stream.ReadNextChar();
                if (IsNotMatterChar(currentChar))
                    continue;

                if (!char.IsLetter(currentChar))
                {
                    prevChar = default; // сбосим предыдущий символ, потому что встаретили НЕ буквы, не значищие символы пропустили выше
                    continue;
                }

                // наш случай
                if (char.ToUpper(prevChar) == char.ToUpper(currentChar))
                {
                    var letterStr = char.ToUpper(currentChar);
                    if (!dictLetter.ContainsKey(letterStr))
                    {
                        dictLetter[letterStr] = new LetterStats { Letter = letterStr };
                    }

                    dictLetter[letterStr].IncStatistic();

                    prevChar = default; // вот тут особенность обработки строки ААА - считаем что предыдущего символа не было
                }
                else
                {
                    prevChar = currentChar;
                }
            }

            return dictLetter.Values.ToList();

        }

        private bool IsNotMatterChar(char c)
        {
            if (char.IsControl(c))
                return true;
            if (c == ' ')
                return true;
            return false;

        }
        private IEnumerable<LetterStats> FilterCharStatsByType(IList<LetterStats> letterStats, CharType charType)
        {
            foreach (var oneLetterStats in letterStats)
            {
                var letterChar = oneLetterStats.Letter; // фильтурем по первой букве, потому что в двойном сравнение буквы одинаковые

                bool isVowel = isVowelChar(letterChar);
                switch (charType)
                {
                    case CharType.Consonants:
                        if (!isVowel)
                            yield return oneLetterStats;
                        break;
                    case CharType.Vowel:
                        if (isVowel)
                            yield return oneLetterStats;
                        break;
                }
            }
        }

        private bool isVowelChar(char c)
        {
            return "aeiouаеёиоуэюяы".IndexOf(c.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

    }
}
