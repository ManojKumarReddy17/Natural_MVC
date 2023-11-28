using System.Data.Common;

#nullable disable
namespace Natural.Core.Models
{
    public class LoginModel
    {
        public string UserName {get; set;}

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
