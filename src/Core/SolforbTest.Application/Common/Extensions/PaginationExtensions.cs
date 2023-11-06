using SolforbTest.Application.Common.Options;

namespace SolforbTest.Application.Common.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<TSource> Page<TSource>(
            this IQueryable<TSource> queryable,
            PaginationOptions paginationOptions
        )
        {
            (int pageSize, int pageNumber) = paginationOptions;
            return pageSize < 0
                ? throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    "Размер страницы не может быть меньше нуля"
                )
                : pageNumber < 0
                    ? throw new ArgumentOutOfRangeException(
                        nameof(pageSize),
                        "Текущая страница не может быть меньше нуля"
                    )
                    : queryable.Skip(pageNumber * pageSize).Take(pageSize);
        }
    }
}
