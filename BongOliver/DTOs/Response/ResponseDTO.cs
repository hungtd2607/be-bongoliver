using BongOliver.Constants;

namespace BongOliver.DTOs.Response
{
    public class ResponseDTO
    {
        public int Code { get; set; } = Constant.SUCCESS_CODE;
        public string Message { get; set; } = Constant.SUCCESS;
        public int Total { get; set; } = 0;
        public Object Data { get; set; } = null;
    }
}
