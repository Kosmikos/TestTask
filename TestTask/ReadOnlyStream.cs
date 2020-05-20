using System.IO;

namespace TestTask
{
    public class ReadOnlyStream : IReadOnlyStream
    {
        private StreamReader _reader;
        private Stream _localStream;

        public bool IsEof
        {
            get
            {
                return _reader.EndOfStream;
            }
        }

        public ReadOnlyStream(Stream stream)
        {
            _localStream = stream;
            _reader = new StreamReader(_localStream);
        }


        public void Dispose()
        {
            if (_localStream != null)
            {
                _localStream.Dispose();
            }
        }

        /// <summary>
        /// Ф-ция чтения следующего символа из потока.
        /// Если произведена попытка прочитать символ после достижения конца файла, метод 
        /// должен бросать соответствующее исключение
        /// </summary>
        /// <returns>Считанный символ.</returns>
        public char ReadNextChar()
        {
            return (char)_reader.Read();
        }

        /// <summary>
        /// Сбрасывает текущую позицию потока на начало.
        /// </summary>
        public void ResetPositionToStart()
        {
            if (_localStream == null)
            {
                return;
            }

            _localStream.Seek(0, SeekOrigin.Begin);
        }
    }
}
