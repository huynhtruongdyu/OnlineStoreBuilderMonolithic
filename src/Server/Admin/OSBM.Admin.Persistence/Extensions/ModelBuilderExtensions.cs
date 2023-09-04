using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Domain.Common;

using System.Linq.Expressions;

namespace OSBM.Admin.Persistence.Extensions;

internal static class ModelBuilderExtensions
{
    internal static ModelBuilder ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var param = Expression.Parameter(entityType.ClrType, "entity");
            var prop = Expression.PropertyOrField(param, nameof(BaseEntity.IsDeleted));
            var entityNotDeleted = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), param);

            entityType.SetQueryFilter(entityNotDeleted);
        }

        return modelBuilder;
    }
}