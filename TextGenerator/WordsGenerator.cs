using Shared;

namespace TextGenerator
{
    public static class WordsGenerator
    {
        private const string _space = " ";
        private const int _repeatSpreading = 64;
        private const int _repeatRate = 4;
        private const int _wordsMinCount = 1;
        private const int _wordsMaxCount = 16;

        private static readonly HashSet<string>? _text;
        private static readonly Dictionary<string, int>? _sentencesToRepeat;

        static WordsGenerator()
        {
            _text = [];
            _sentencesToRepeat = [];
        }

        public static string Generate()
        {
            string sentence;

            if (RandomHelper.Instance.Next(1, _repeatSpreading) is 1 && _sentencesToRepeat!.Count > 0)
            {
                sentence = _sentencesToRepeat.ElementAt(Index.FromStart(RandomHelper.Instance.Next(0, _sentencesToRepeat.Count))).Key;

                if (_sentencesToRepeat[sentence] == 1)
                    _sentencesToRepeat.Remove(sentence);
                else
                    _sentencesToRepeat[sentence] -= 1;

                return sentence;
            }

            var wordsCount = WordsCount;

            sentence = string.Join(_space, Words.Take(RandomHelper.Instance.Next(wordsCount, wordsCount))).FirstLetterToUppercase();

            if (_sentencesToRepeat!.Count < Globals.Kibi)
                _sentencesToRepeat.TryAdd(sentence, RandomHelper.Instance.Next(1, _repeatRate));

            return sentence;
        }

        private static int WordsCount =>
            RandomHelper.Instance.Next(_wordsMinCount, _wordsMaxCount);

        private static IEnumerable<string> Words =>
            _text!.OrderBy(x => RandomHelper.Instance.Next());

        private static string FirstLetterToUppercase(this string str) =>
            char.ToUpper(str[0]) + str.Substring(1);

        public static void LoadDictionary(string path)
        {
            using var reader = new StreamReader(path);
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrWhiteSpace(line))
                    _text!.Add(line);
            }

            reader.Close();
        }
    }
}
