namespace TextGenerator
{
    internal static class RandomHelper
    {
        private static readonly Random? _random;

        internal static Random Instance
        {
            get
            {
                return _random ?? new Random();
            }
        }
    }
}
