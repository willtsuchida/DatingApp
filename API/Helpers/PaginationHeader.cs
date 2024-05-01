namespace API;

//vai ser um objeto que vamos retornar dentro do http responde header, chamado Pagination
public class PaginationHeader
{
    //criar um extension method que vai extender o HTTP
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages) 
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }

    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
