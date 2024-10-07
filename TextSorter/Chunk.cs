using System.Text;

namespace TextSorter
{
    internal static class Chunk
    {
        private static readonly string _chunkFilename = "chunk_{0}.txt";

        internal static List<string> CreateChunks(string inputFilename, string chunksPath, long chunkSizeBytes)
        {
            var chunkFiles = new List<string>();
            var lines = new List<string>();

            using (var reader = new StreamReader(inputFilename!))
            {
                var chunkIndex = 0;
                var actualFileSize = 0L;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine()!.Transform();
                    lines.Add(line);

                    actualFileSize += Encoding.UTF8.GetByteCount(line!);

                    if (actualFileSize >= chunkSizeBytes)
                    {
                        SortAndWriteChunk(chunksPath, lines, chunkFiles, chunkIndex++);

                        lines.Clear();
                        actualFileSize = 0;
                    }
                }

                if (lines.Count > 0)
                    SortAndWriteChunk(chunksPath, lines, chunkFiles, chunkIndex++);
            }

            return chunkFiles;
        }

        internal static void MergeSortedChunks(List<string> chunkFilesList, string outputFilename)
        {
            var readers = new List<StreamReader>();

            foreach (var chunkFile in chunkFilesList)
                readers.Add(new StreamReader(chunkFile));

            using var writer = new StreamWriter(outputFilename!, false, Encoding.UTF8);

            var lines = readers.Select(r => r.ReadLine()).ToList();
            while (lines.Count > 0)
            {
                int minIndex = 0;
                for (int i = 1; i < lines.Count; i++)
                {
                    if (string.Compare(lines[i]!, lines[minIndex]!) < 0)
                        minIndex = i;
                }

                writer.WriteLine(lines[minIndex]!.TransformBack());

                lines[minIndex] = readers[minIndex].ReadLine();

                if (lines[minIndex] is null)
                {
                    lines.RemoveAt(minIndex);

                    readers[minIndex].Dispose();
                    readers.RemoveAt(minIndex);
                }
            }

            writer.Close();
        }

        private static void SortAndWriteChunk(string chunksPath, List<string> lines, List<string>? chunkFiles, int chunkIndex)
        {
            string chunkFilename = SetChunkFilename(chunksPath, chunkIndex);

            lines.Sort(string.Compare);
            File.WriteAllLines(chunkFilename, lines);

            chunkFiles!.Add(chunkFilename);
        }

        private static string SetChunkFilename(string chunksPath, int chunkIndex) =>
            Path.Combine(chunksPath!, string.Format(_chunkFilename, chunkIndex++));
    }
}
