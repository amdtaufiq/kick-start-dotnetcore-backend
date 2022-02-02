namespace BEService.Core.CustomEntities
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public Message Message { get; set; }
        public T Data { get; set; }
        public Metadata Meta { get; set; }

    }
}
