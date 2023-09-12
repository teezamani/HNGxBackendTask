namespace HNGBACKENDTrack.Dto
{
    public class BaseResponseDto
    {
        public BaseResponseDto(bool status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
