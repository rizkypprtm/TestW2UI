using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestW2uiWeb.Model
{

    public class UserModel
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }

        public string message { get; set; }
        public Errors errors { get; set; }
        public int status_code { get; set; }


        public class Errors
        {
            public List<string> email { get; set; }
        }

    }

    public class CreateUserModelParameters
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string repeatPassword { get; set; }
    }

    public class UpdateUserModelParameters
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
    }

    public class ChangePasswordModelParameters
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string repeatPassword { get; set; }
    }

}
