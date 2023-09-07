using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSBM.Admin.Shared.Models.ApiRequest;

public class PaginationApiRequestModel
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }

    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 1;
}