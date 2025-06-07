namespace MotorcycleRentalService.Application.Responses
{
    public class QueryResponse<T> : BaseResponse where T : class
    {
        public IEnumerable<T> Objects { get; set; }
    }
}
