namespace SolforbTest.Application.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) =>
            enumerable?.Any() != true;
    }
}
