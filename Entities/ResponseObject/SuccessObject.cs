namespace Entities.ResponseObject
{
    public class SuccessObject<T> where T : class
    {
        public string? Message { get; set; }
        public T? Data { get; set; } = null;
    }
}
