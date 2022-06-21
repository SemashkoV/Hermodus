
using Hermodus.Data;
using Hermodus.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Repo
{
    public class FormsAuthenticationProvider : IAuthentication
    {
        EFDbContext context = new EFDbContext();
        public User Authenticate(string username, string password)
        {

            User result = context.Users.FirstOrDefault(u => u.Email == username &&
                                                       u.Password == password);
            return result;

        }

        public bool Logout()
        {
            return true;
        }
    }
}
