using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration config;

        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=ToDoList;Integrated Security=True;";
        public UserRL(IConfiguration config)
        {
            this.config = config;
        }

        public bool Register(UserRegistrationModel registrationModel)
        {
            //sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            sqlConnection = new SqlConnection(ConnString);

            using (sqlConnection)
                try
                {
                    //var password = this.EncryptPassword(registrationModel.Password);
                    var password = registrationModel.Password;
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_Register", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@Fullname", registrationModel.Fullname);
                    sqlCommand.Parameters.AddWithValue("@Email", registrationModel.Email);
                    sqlCommand.Parameters.AddWithValue("@Mobile", registrationModel.Mobile);
                    sqlCommand.Parameters.AddWithValue("@Password", registrationModel.Password);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        //JWT Method
        public string GenerateJWTToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.UtcNow.AddDays(11),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //

        public string GenerateJWTToken2(string email, int userid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email),
                new Claim("Id", userid.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(11),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string Login(UserLoginModel loginModel)
        {

            using (sqlConnection = new SqlConnection(ConnString))
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_Login", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@Email", loginModel.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", loginModel.Password);

                    SqlDataReader rd = sqlCommand.ExecuteReader();
                    //if (rd.HasRows)
                    rd.Close();
                    if (rd != null)
                        {
                        //while (rd.Read())
                        //{
                        //    loginModel.Email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        //    loginModel.Password = Convert.ToString(rd["Password"] == DBNull.Value ? default : rd["Password"]);
                        //}
                            string query = "SELECT Id FROM Users WHERE Email = '" + loginModel.Email + "'";
                            SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        cmd.CommandType = System.Data.CommandType.Text;
                            var ID =cmd.ExecuteScalar();
                        int Id = Convert.ToInt32(ID);
                            


                            var token = this.GenerateJWTToken2(loginModel.Email, Id);
                        return token;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally { sqlConnection.Close(); }
        }

    }
}
