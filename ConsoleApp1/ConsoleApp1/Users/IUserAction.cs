using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Notes
{

    public interface IUserAction
    {
        void AddUser(User user);
        List<User> GetUserList();
        void DeleteUser(string phoneNumber);
        List<User> GetUserByFilter(string filter);
    }

}
