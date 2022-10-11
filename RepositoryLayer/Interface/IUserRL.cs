using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public bool Register(UserRegistrationModel registrationModel);

        public string Login(UserLoginModel loginModel);
    }
}
