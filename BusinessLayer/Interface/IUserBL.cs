using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public bool Register(UserRegistrationModel registrationModel);

        public string Login(UserLoginModel loginModel);
    }
}
