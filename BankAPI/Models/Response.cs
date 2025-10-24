namespace BankAPI.Models
{
    public class Response
    {
        public string RequestId;
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

        // Adittioned for permission use it response.Data = null;
        public object? Data { get; set; }
    }
}
