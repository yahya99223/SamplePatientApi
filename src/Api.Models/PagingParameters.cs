using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class PagingParameters
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page value should be a valid positive integer")]
        public int Page { get; set; } = 1;
        [Range(1, 100, ErrorMessage = "Page size should be between {1} and {2}, default is 10")]
        public int PageSize { get; set; } = 10;
    }
}
