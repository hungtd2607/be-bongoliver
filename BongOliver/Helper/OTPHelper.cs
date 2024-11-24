namespace BongOliver.Helper
{
    public class OTPHelper
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime Expire { get; set; }
        public OTPHelper() { }
        public static string GenerateOtp(int length = 6)
        {
            Random random = new Random();
            string otp = string.Empty;

            for (int i = 0; i < length; i++)
            {
                otp += random.Next(0, 10);
            }

            return otp;
        }
    }
}
