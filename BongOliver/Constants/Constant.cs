namespace BongOliver.Constants
{
    public class Constant
    {
        public const string SUCCESS = "Success";
        public const int SUCCESS_CODE = 200;
        public const int FAILED_CODE = 400;


        public const int STATUS_NEW = 0;
        public const int STATUS_INPROGRESS = 1;
        public const int STATUS_DONE = 2;
        public const int STATUS_PENDDING = 3;
        public const int STATUS_CANCEL = 4;


        public const int ROLE_ADMIN = 1;
        public const int ROLE_USER = 2;
        public const int ROLE_STYLIST = 3;

        public const string ROLE_NAME_ADMIN = "ADMIN";
        public const string ROLE_NAME_USER = "USER";
        public const string ROLE_NAME_STYLIST = "STYLIST";


        public const bool MALE = false;
        public const bool FEMALE = true;
        public const bool NOT_VERIFY = true;
        public const bool VERIFY = false;
        public const bool NOT_DELETE = true;
        public const bool DELETE = false;

        public const string DEFAULT_AVATAR = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png";
    }
}
