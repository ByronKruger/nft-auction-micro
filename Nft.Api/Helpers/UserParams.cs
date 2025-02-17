public class UserParams
{
    private const int MaxPageSize = 20;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int UserId {get; set;}

    public string OrderByUsername { get; set; }
    public string FilterForOwner { get; set; }
}