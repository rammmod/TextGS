namespace TextSorter
{
    internal static class StringExtensions
    {
        private const string _dot = ".";
        private const string _space = " ";
        private const char _zero = '0';

        private static readonly int _digitsLength = int.MaxValue.ToString().Length;

        public static string Transform(this string str)
        {
            var dotIndex = str.IndexOf(_dot);
            return string.Concat(str.AsSpan(dotIndex + 2, str.Length - 2 - dotIndex), _space, string.Format(str[..dotIndex].PadLeft(_digitsLength, _zero)));
        }

        public static string TransformBack(this string str) =>
            string.Concat(str.Substring(str.Length - _digitsLength, _digitsLength).TrimStart(_zero), _dot, _space, str.AsSpan(0, str.Length - _digitsLength - 1));
    }
}
