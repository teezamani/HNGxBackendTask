namespace HNGBACKENDTrack.Dto
{
    public class BaseResponseDto
    {  
        public BaseResponseDto(bool status , int? status_code = null, string? message = null , object? data = null)
        {
            Status = status;
            Status_code = status_code;
            Message = message;
            Data = data;
        }

        public bool Status { get; set; }
        public int? Status_code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
