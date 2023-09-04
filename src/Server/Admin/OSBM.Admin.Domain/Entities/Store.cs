using OSBM.Admin.Domain.Common;

namespace OSBM.Admin.Domain.Entities;

public class Store : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}