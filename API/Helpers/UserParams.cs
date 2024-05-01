namespace API;

public class UserParams
{
    //set maximum page size that client can request
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1; //always return first page unless told otherwhise
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string CurrentUsername { get; set; }
    public string Gender { get; set; }
    public int MinAge { get; set; } = 18;
    public int MaxAge { get; set; } = 100;
    public string OrderBy { get; set; } = "lastActive";

}
