using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Domain;

namespace SolforbTest.Application.Common.Extensions
{
    public static class DatasetExtensions
    {
        public static async Task<T> GetByIdOrThrow<T>(
            this IQueryable<T> queryable,
            int id,
            CancellationToken cancellationToken = default
        )
            where T : IEntity
        {
            return await queryable.FirstOrDefaultAsync(o => o.Id == id, cancellationToken)
                ?? throw new NotFoundException(typeof(T).Name, id);
        }

        public static async Task ThrowIfDoesntExist<T>(
            this IQueryable<T> queryable,
            int id,
            CancellationToken cancellationToken = default
        )
            where T : IEntity
        {
            bool entityExists = await queryable.AnyAsync(o => o.Id == id, cancellationToken);
            if (!entityExists)
                throw new NotFoundException(typeof(T).Name, id);
            return;
        }
    }
}
