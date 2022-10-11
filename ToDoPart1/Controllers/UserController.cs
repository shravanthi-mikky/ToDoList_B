﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ToDoPart1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL iUserBL;
        public UserController(IUserBL iUserBL)
        {
            this.iUserBL = iUserBL;
        }

        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistrationModel registrationModel)
        {
            try
            {
                var result = iUserBL.Register(registrationModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Registration Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }


        [HttpPost("Login")]
        public IActionResult Login(UserLoginModel loginModel)
        {
            try
            {
                var result = iUserBL.Login(loginModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }

    }
}
