namespace WPFApp.Domain.Http.Responses
{
    public class Response
    {
        public bool Success { get; set; }
        public string[] Messages { get; set; } 
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }

}
