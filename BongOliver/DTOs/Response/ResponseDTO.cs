using BongOliver.Constants;

namespace BongOliver.DTOs.Response
{
    public class ResponseDTO
    {
        public int Code { get; set; } = AppConst.SUCCESS_CODE;
        public string Message { get; set; } = AppConst.SUCCESS;
        public int Total { get; set; } = 0;
        public Object Data { get; set; } = null;
    }
}
