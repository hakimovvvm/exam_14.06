namespace Domain.ApiResponse;

public class PagedResponse<T> : Response<T>
{
    public int TotalResult { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PagedResponse(T data, int totalResult, int pageNumber, int pageSize) : base(data)
    {
        TotalResult = totalResult;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling((double)totalResult / pageSize);
    }
}
