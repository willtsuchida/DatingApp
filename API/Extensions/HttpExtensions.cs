using System.Text.Json;
using Newtonsoft.Json;

namespace API;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
    {
        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        response.Headers.Add("Pagination", System.Text.Json.JsonSerializer.Serialize(header, jsonOptions));
        //because is a custom header, tem que dar allow no CORS policy
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}
