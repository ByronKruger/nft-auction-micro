using Microsoft.EntityFrameworkCore;

namespace NftApi.Helpers;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
 
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count/ (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync(); // must be here and not after pagination
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return new PagedList<T>(await items.ToListAsync(), count, pageNumber, pageSize);
    }
}