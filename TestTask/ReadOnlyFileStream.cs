using System.IO;

namespace TestTask
{
    public class ReadOnlyFileStream : ReadOnlyStream
    {
        /// <summary>
        /// Конструктор класса. 
        /// Т.к. происходит прямая работа с файлом, необходимо 
        /// обеспечить ГАРАНТИРОВАННОЕ закрытие файла после окончания работы с таковым!
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        public ReadOnlyFileStream(string fileFullPath) :base(new FileStream(fileFullPath, FileMode.Open))
        {
        }        
    }
}
