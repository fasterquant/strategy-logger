using System.IO;

namespace FasterQuant.StrategyLogger.Tests
{
    internal static class LogFileHandler
    {
        internal static string Read(string path, string fileName)
        {
            var logFileContents = "";
            var s = "";
            var fullPath = Path.Combine(path, fileName);
            using (StreamReader sr = File.OpenText(fullPath))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    logFileContents += s;
                }
            }

            return logFileContents;
        }

        internal static void Delete(string path, string fileName)
        {
            File.Delete(Path.Combine(path, fileName));
        }

    }
}