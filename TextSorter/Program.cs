using Shared;

namespace TextSorter
{
    internal class Program
    {
        private static long _chunkSizeBytes;
        private static string? _chunksPath;
        private static string? _outputFilename;
        private static string? _inputFilename;

        static void Main(string[] args)
        {
            SetArgs(args);

            var chunkFilesList = Chunk.CreateChunks(_inputFilename!, _chunksPath!, _chunkSizeBytes);

            Chunk.MergeSortedChunks(chunkFilesList, _outputFilename!);

            foreach (var file in chunkFilesList)
                File.Delete(file);

            Directory.Delete(_chunksPath!);
        }

        private static void SetArgs(string[] args)
        {
            if (long.TryParse(args[0], out var chunkSize))
            {
                _chunkSizeBytes = chunkSize * Globals.Kibi * Globals.Kibi * Globals.Kibi;
            }
            else
                throw new Exception(string.Format(Globals.ErrorMessage, "Chunk size is not set."));

            if (!Path.IsPathFullyQualified(args[1]))
                throw new Exception(string.Format(Globals.ErrorMessage, "Chunks path is not valid."));

            Directory.CreateDirectory(args[1]);
            _chunksPath = args[1];

            if (File.Exists(args[2]))
                _inputFilename = args[2];
            else
                throw new Exception(string.Format(Globals.ErrorMessage, "Input file is not found."));

            if (!Path.IsPathFullyQualified(args[3]))
                throw new Exception(string.Format(Globals.ErrorMessage, "Output file path is not valid."));

            _outputFilename = args[3];
        }
    }
}
