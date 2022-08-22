using LectureSchedule.API.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LectureSchedule.API.Extensions
{
    public static class PaginationExtensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage,
            int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage,
                totalItems, totalPages);
            var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(
                paginationHeader,
                options
            ));
            response.Headers.Add("Acess-Control-Expose-Headers", "Pagination");
        }
    }
}
