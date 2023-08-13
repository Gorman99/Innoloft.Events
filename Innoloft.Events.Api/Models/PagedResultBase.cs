namespace Innoloft.Events.Api.Models;

public abstract class PagedResultBase
{
    /// <summary>
    ///  
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int TotalCount { get; set; }

    // /// <summary>
    // ///
    // /// </summary>
    public int FirstRowOnPage => (PageIndex - 1) * PageSize + 1;
    //
    // /// <summary>
    // ///
    // /// </summary>
    public int LastRowOnPage => Math.Min(PageIndex * PageSize, TotalCount);
}