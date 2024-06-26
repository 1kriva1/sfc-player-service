﻿using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Common.Models.Sorting;

namespace SFC.Player.Application.Features.Common.Models;
public class FindParameters<T>
{
    public Filters<T> Filters { get; set; } = default!;

    public Sortings<T> Sorting { get; set; } = default!;

    public Pagination Pagination { get; set; } = default!;
}
