using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

public class PageNation<TModel>
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }

    public PageNation(List<TModel> data, IConfiguration configuration, int page)
    {
        TotalItems = data.Count;
        PageSize = configuration.GetSection("Pagination:PageSize").Get<int>();
        TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
        CurrentPage = page;
        StartPage = 1;
        EndPage = TotalPages;
    }

    public List<TModel> GetPaginatedData(List<TModel> data)
    {
        var paginatedData = data.Skip((CurrentPage - 1) * PageSize)
                                .Take(PageSize)
                                .ToList();

        return paginatedData;
    }
}
