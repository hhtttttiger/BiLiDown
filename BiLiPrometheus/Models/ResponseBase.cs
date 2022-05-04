namespace BiLiPrometheus.Models
{
    public class ResponseBase<T>
    {
        public int code { get; set; }
        public string message { get; set; }

        public int ttl { get; set; }

        public T data { get; set; }
    }
}
