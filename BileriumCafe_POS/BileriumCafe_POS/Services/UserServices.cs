using BileriumCafe_POS.Models;
using BileriumCafe_POS.Utils;
using System.Text.Json;
using System.Data;

namespace BileriumCafe_POS.Services
{
    public class UserServices
    {

        private List<User> _users = new()
        {
            new User()
            {

                Password = "admin",
                Role = "admin",
            },

            new User()
            {

                Password = "staff",
                Role = "staff"
            }
        };


        //SAve users in json





        //Login

        public User LoginUser(string password)
        {
            const string errorMsg = "Invalid Password ";

            User user = _users.FirstOrDefault(u => u.Password == password);

            return user ?? throw new Exception(errorMsg);
        }

    }
}