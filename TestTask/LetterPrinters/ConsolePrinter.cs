using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.LetterPrinters
{
    public class ConsolePrinter : ILetterPrinter
    {
        /// <summary>
        /// Ф-ция выводит на экран полученную статистику в формате "{Буква} : {Кол-во}"
        /// Каждая буква - с новой строки.
        /// Выводить на экран необходимо предварительно отсортировав набор по алфавиту.
        /// В конце отдельная строчка с ИТОГО, содержащая в себе общее кол-во найденных букв/пар
        /// </summary>
        /// <param name="letters">Коллекция со статистикой</param>
        public void PrintStatistic(IEnumerable<LetterStats> letters)
        {
            var sumCount = 0;
            foreach (var oneLetterStat in letters.OrderBy(l => l.Letter))
            {
                Console.WriteLine($"{oneLetterStat.Letter} : {oneLetterStat.Count}");
                sumCount = oneLetterStat.Count + sumCount; //=+ или += зачем тут эти ухищрения - так понятней
            }

            Console.WriteLine($"ИТОГО : {sumCount}");
        }

    }
}
