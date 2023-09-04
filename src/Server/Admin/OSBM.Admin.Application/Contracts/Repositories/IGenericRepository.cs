using OSBM.Admin.Domain.Common;

namespace OSBM.Admin.Application.Contracts.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
}