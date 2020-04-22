using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.ApplicationModel;

namespace Utilities.Logging
{
    public class Logger : ILogger
    {
        private readonly string _fileName;

        private LogLevels _selectedLogLevel = LogLevels.All;

        public Logger()
        {
            var now = DateTime.Now;
            var version = Package.Current.Id.Version;
            _fileName = $"KrinklesHell_{now.Year}-{now.Month}-{now.Day}-{now.Hour}-{now.Minute}-{now.Second}.log";

            LogMessage($"KrinklesHell - version {version.Major}.{version.Minor}.{version.Build}.{version.Revision}");
            LogMessage($"Log {now.Day}-{now.Month}-{now.Year} at {now.Hour}:{now.Minute}");
            LogMessage(new string('-', 100));
        }

        public void LogMessage(string message)
        {
            Log($"{message}", LogLevels.Message);
        }

        public void Fatal(string fatalMessage, Exception exception = null)
        {
            Log(FormatMessage(fatalMessage, exception), LogLevels.Fatal);
        }

        public void Error(string errorMessage, Exception exception = null)
        {
            Log(FormatMessage(errorMessage, exception), LogLevels.Error);
        }

        public void Info(string infoMessage, Exception exception = null)
        {
            Log(FormatMessage(infoMessage, exception), LogLevels.Info);
        }

        public void Trace(string traceMessage, Exception exception = null)
        {
            Log(FormatMessage(traceMessage, exception), LogLevels.Trace);
        }

        public void Warn(string warningMessage, Exception exception = null)
        {
            Log(FormatMessage(warningMessage, exception), LogLevels.Warn);
        }

        private string FormatMessage(string message, Exception exception = null, [CallerMemberName]string LogType = "")
        {
            var formattedLogMessage = $"|{LogType.ToUpper()}|\t --> {message}";
            if (exception != null)
            {
                formattedLogMessage = $"{formattedLogMessage} <--> {exception.GetType()} - {exception.Message}";
            }

            return $"{formattedLogMessage}";
        }

        private void Log(string message, LogLevels level)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

#if DEBUG
            if (_selectedLogLevel >= level)
            {
                Debug.WriteLine(message);
            }
#endif

            WriteToFile(message);
        }

        private async void WriteToFile(string message)
        {
            message = $"{message}\n";

            var localCache = Windows.Storage.ApplicationData.Current.LocalCacheFolder;
            var logFile = await localCache.CreateFileAsync(_fileName, Windows.Storage.CreationCollisionOption.OpenIfExists);

            using (var fileStream = File.OpenWrite(logFile.Path))
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                fileStream.Seek(0, SeekOrigin.End);
                fileStream.Write(bytes, 0, bytes.Length);
            }

            {
                // It's also possible to use a stream writer. However it seems slower than handeling the stream directly
                //using (IRandomAccessStream racs = await logFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
                //{
                //    racs.Seek(racs.Size);
                //    using (var tw = new StreamWriter(racs.AsStreamForWrite()))
                //    {
                //        tw.WriteLine(message);
                //    }
                //}
            }
        }
    }
}