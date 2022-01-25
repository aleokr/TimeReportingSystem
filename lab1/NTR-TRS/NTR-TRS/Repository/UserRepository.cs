using Newtonsoft.Json;
using NTR_TRS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Repository
{
    public class UserRepository
    {
        private static string usersFilePath = "../NTR-TRS/Files/users.json";


        public List<User> GetAllUsers()
        {
            using (StreamReader r = new StreamReader(usersFilePath))
            {
                string json = r.ReadToEnd();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                return users;
            }
        }
    }
}
