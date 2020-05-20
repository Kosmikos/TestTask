using System;
using TestTask.Analyz;
using TestTask.LetterPrinters;

namespace TestTask
{
    public class Program
    {
        static string info = @"===== Программа для подсчета вхождений букв в файлах ===== 
       Запуск без ключей:
         Программа принимает на входе 2 пути до файлов в кодировке UTF-8.
         Анализирует в первом файле кол-во вхождений каждой согласной буквы (регистрозависимо). Например А, б, Б, Г и т.д.
         Анализирует во втором файле кол-во вхождений парных гласных букв (не регистрозависимо). Например АА, Оо, еЕ, тт и т.д.
         По окончанию работы - выводит данную статистику на экран.
        
       -h - показать описание программы";

        static void Main(string[] args)
        {
            var key = "";
            if (args.Length > 0) key = args[0];

            switch (key)
            {
                case "-h":
                    ShowHelp();
                    break;

                default:
                    if (args.Length < 2)
                    {
                        ShowHelp("Неверные параметры запуска");
                        return;
                    }

                    try
                    {
                        var consolePrinter = new ConsolePrinter();
                        string fileFullPath = args[0];
                        using (var streamData = new ReadOnlyFileStream(fileFullPath))
                        {
                            var letterAnalyzer = new LetterAnalyzer(streamData);
                            letterAnalyzer.PrintSingleLetterStatistic(consolePrinter, CharType.Consonants);
                        }

                        fileFullPath = args[1];
                        using (var streamData = new ReadOnlyFileStream(fileFullPath))
                        {
                            var letterAnalyzer = new LetterAnalyzer(streamData);
                            letterAnalyzer.PrintDoubleLetterStatistic(consolePrinter, CharType.Vowel);
                        }


                        // 1 начальная логика программы - не поддается тестированию
                        // 2 удаление типов букв и вывод - трудная логика - заменена на вывод указанных букв - это проще

                        //IReadOnlyStream inputStream1 = GetInputStream(args[0]);
                        //IReadOnlyStream inputStream2 = GetInputStream(args[1]);

                        //IList<LetterStats> singleLetterStats = FillSingleLetterStats(inputStream1);
                        //IList<LetterStats> doubleLetterStats = FillDoubleLetterStats(inputStream2);

                        //RemoveCharStatsByType(singleLetterStats, CharType.Vowel);
                        //RemoveCharStatsByType(doubleLetterStats, CharType.Consonants);

                        //PrintStatistic(singleLetterStats);
                        //PrintStatistic(doubleLetterStats);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                    Console.ReadKey();
                    break;
            }
        }

        static void ShowHelp(string msg = "")
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Console.WriteLine(msg);
                Console.WriteLine();
            }
            Console.WriteLine(info);
        }
    }
}
