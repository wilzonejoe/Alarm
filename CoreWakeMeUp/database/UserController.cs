using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.database
{
    /**
  * UserController.cs
  * Controller class used to encapsulate user
  * functions 
  */

    // import sql controller here

    public class UserController
    {
        private static UserController Instance;

        private UserController() { }

//        public static UserController Instance
//        {
//            get
//            {
//                if (Instance == null)
//                {
//                    Instance = new Singleton();
//                }
//                return Instance;
//            }
//        }
        public bool RegisterUser(string username, string password)
        {
            bool success = false;
            //  get new user commend

            // call sql database controller class try catch

            // catch

            // return result
            return success;
        }

        public bool EditUser()
        {
            bool success = false;
            //  get new user commend

            // call sql database controller class try catch

            // catch

            // return result
            return success;
        }


    }
}
