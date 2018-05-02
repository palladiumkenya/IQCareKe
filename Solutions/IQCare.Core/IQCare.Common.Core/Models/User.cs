namespace IQCare.Common.Core.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public int DeleteFlag { get; set; }
    }
}