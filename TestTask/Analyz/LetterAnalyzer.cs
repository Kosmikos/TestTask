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

        public void PrintSingleLetterStatistic(ILetterPrinter letterPrinter, CharType useOnlyThisCharType)
        {
            IList<LetterStats> singleLetterStats = FillSingleLetterStats();

            var filteredLetter=FilterCharStatsByType(singleLetterStats, useOnlyThisCharType);
            letterPrinter.Print(filteredLetter);
        }

        public void PrintDoubleLetterStatistic(ConsolePrinter consolePrinter, CharType charType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения каждой буквы.
        /// Статистика РЕГИСТРОЗАВИСИМАЯ!
        /// </summary>
        /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
        /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
        private IList<LetterStats> FillSingleLetterStats()
        {
            var dictLetter = new Dictionary<char, LetterStats>();
            _stream.ResetPositionToStart();
            while (!_stream.IsEof)
            {
                char c = _stream.ReadNextChar();
                if (char.IsControl(c))
                    continue;

                if (!dictLetter.ContainsKey(c))
                {
                    dictLetter[c] = new LetterStats { Letter = c.ToString() };
                }

                IncStatistic(dictLetter[c]);
            }

            return dictLetter.Values.ToList();
        }

        /// <summary>
        /// Метод увеличивает счётчик вхождений по переданной структуре.
        /// </summary>
        /// <param name="letterStats"></param>
        private void IncStatistic(LetterStats letterStats)
        {
            letterStats.Count++;
        }

        private IEnumerable<LetterStats> FilterCharStatsByType(IList<LetterStats>  letterStats, CharType charType)
        {
            foreach(var oneLetterStats in letterStats)
            {
                var firstChar = oneLetterStats.Letter[0];

                bool isVowel = isVowelChar(firstChar);
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
