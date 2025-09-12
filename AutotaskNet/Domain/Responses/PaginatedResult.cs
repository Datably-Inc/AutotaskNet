namespace AutotaskNet.Domain.Responses;

internal class PaginatedResult<T>
{
    public List<T> Items { get; set; }
    public PagingModel PageDetails { get; set; }

    public record PagingModel
    {
        public int Count { get; set; }
        public int RequestCount { get; set; }
        public string? PrevPageUrl { get; set; }
        public string? NextPageUrl { get; set; }
    }
}