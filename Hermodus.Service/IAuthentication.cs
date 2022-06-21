
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
   public interface IAuthentication
    {
        User Authenticate(string username, string password);
        bool Logout();
    }
}
