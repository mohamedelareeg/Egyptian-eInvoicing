using System.Collections;
using System.Collections.Generic;

public class CustomList<T> : IEnumerable<T>
{
    public int Count => Items.Count;
    public int? TotalCount { get; set; }
    public int? TotalPages { get; set; }

    public List<T> Items { get; set; }

    public CustomList(List<T> items, int? totalCount, int? totalPages)
    {
        Items = items;
        TotalCount = totalCount;
        TotalPages = totalPages;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
