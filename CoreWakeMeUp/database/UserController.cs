using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CoreWakeMeUp.Entity;

namespace CoreWakeMeUp.database
{
    /**
  * UserController.cs
  * Controller class used to encapsulate user
  * functions 
  */

    public class UserController
    {
        private static UserController _instance;

        private UserController()
        {
        }

        public static UserController Instance()
        {
            if (_instance == null)
                _instance = new UserController();
            return _instance;
        }


        public bool RegisterUser(string username, string password)
        {
            var success = false;
            //function to register a consumer
            return success;
        }

        public bool EditUser(User user)
        {
            var success = false;
            //function to update consumer
            return success;
        }
    }
}