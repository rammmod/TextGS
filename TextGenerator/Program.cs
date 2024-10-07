using System.Text;
using Shared;

namespace TextGenerator
{
    internal class Program
    {
        private static long _fileSizeBytes;
        private static string? _outputFilename;
        private static string? _dictionaryPath;

        private static readonly string _lineFormat = "{0}. {1}";

        static void Main(string[] args)
        {
            SetArgs(args);

            WordsGenerator.LoadDictionary(_dictionaryPath!);

            using var writer = new StreamWriter(_outputFilename!, false, Encoding.UTF8);

            var actualFileSize = 0L;
            try
            {
                while (actualFileSize <= _fileSizeBytes)
                {
                    int number = RandomHelper.Instance.Next();
                    string randomString = WordsGenerator.Generate();

                    string line = string.Format(_lineFormat, number, randomString);

                    writer.WriteLine(line);

                    actualFileSize += Encoding.UTF8.GetByteCount(line);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Globals.ErrorMessage, ex?.ToString()));
            }
            finally
            {
                writer.Close();
            }
        }

        private static void SetArgs(string[] args)
        {
            if (Path.IsPathFullyQualified(args[0]))
                _outputFilename = args[0];
            else
                throw new Exception(string.Format(Globals.ErrorMessage, "Generated file path is not well defined."));

            if (File.Exists(args[1]))
                _dictionaryPath = args[1];
            else
                throw new Exception(string.Format(Globals.ErrorMessage, "Dictionary file is not found."));

            if (long.TryParse(args[2], out var fileSizeGB))
                _fileSizeBytes = fileSizeGB * Globals.Kibi * Globals.Kibi * Globals.Kibi;
            else
                throw new Exception(string.Format(Globals.ErrorMessage, "File size is not set."));
        }
    }
}
