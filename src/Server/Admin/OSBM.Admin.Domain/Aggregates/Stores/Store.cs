using OSBM.Admin.Domain.Common;

namespace OSBM.Admin.Domain.Aggregates.Stores;

public class Store : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}