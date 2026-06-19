namespace DVLD_API.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDTO()
        {
            Success = false;
            Message = "";
            Data = null;
        }

        public ResponseDTO(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}