namespace Innoloft.Events.Api.Models;

public class PagedResult<T> : PagedResultBase where T : class
{
    /// <summary>
    ///
    /// </summary>
    public IList<T> Results { get; set; }

    /// <summary>
    ///
    /// </summary>
    public PagedResult()
    {
        Results = new List<T>();
    }
}